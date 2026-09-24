# React + .NET API secured with Microsoft Entra ID

A demo of a React single-page app that signs users in (or lets them create an account)
with **Microsoft Entra ID** using OpenID Connect, and a **.NET 8 Web API** that only
accepts calls carrying a valid Entra-issued OAuth 2.0 access token.

```
┌──────────────────────┐   1. sign in / sign up (OIDC, auth code + PKCE)   ┌──────────────────┐
│  React SPA (web/)    │ ───────────────────────────────────────────────▶ │ Microsoft Entra  │
│  MSAL React          │ ◀─────────────── ID token + access token ──────── │ ID / External ID │
│  localhost:5173      │                                                   └──────────────────┘
└─────────┬────────────┘                                                            │
          │ 2. Authorization: Bearer <access token>                                 │ signing keys
          ▼                                                                         ▼ (OIDC metadata)
┌──────────────────────┐  3. validate signature, issuer, audience, expiry, scope
│  .NET API (api/)     │ ─────────────────────────────────────────────────────────▶
│  Microsoft.Identity  │  401 if missing/invalid token · 403 if scope missing
│  .Web · :5016        │
└──────────────────────┘
```

## What's in here

| Path | What it is |
| --- | --- |
| `web/` | React 19 + Vite + TypeScript SPA using `@azure/msal-react` / `@azure/msal-browser` v5 |
| `api/EntraDemo.Api/` | ASP.NET Core 8 minimal API using `Microsoft.Identity.Web` for JWT bearer validation |
| `api/EntraDemo.Api.Tests/` | Integration tests proving unauthenticated / invalid / under-scoped calls are rejected |

### API endpoints

| Method & path | Auth | Description |
| --- | --- | --- |
| `GET /api/health` | Public | Liveness check |
| `GET /api/me` | Token + `access_as_user` scope | Returns the caller's identity and claims from the validated token |
| `GET /api/todos` | Token + scope | Lists the caller's todos |
| `POST /api/todos` | Token + scope | Creates a todo `{ "title": "..." }` |
| `PATCH /api/todos/{id}` | Token + scope | Updates `{ "title"?, "done"? }` |
| `DELETE /api/todos/{id}` | Token + scope | Deletes a todo |

Todos are kept in memory and partitioned by the token's `oid` (user object id), so each user
only sees their own. Swap `TodoStore` for a database for anything real.

### How the security works

**API** (`api/EntraDemo.Api/Program.cs`)
- `AddMicrosoftIdentityWebApi` validates the bearer token's signature (keys from the tenant's
  OIDC metadata), issuer, audience (`<client-id>` or `api://<client-id>`) and lifetime.
- A **fallback authorization policy** requires an authenticated user holding the
  `access_as_user` scope on *every* endpoint unless it opts out with `AllowAnonymous()`.
  New endpoints are secure by default.
- CORS allows only the SPA's origin.

**SPA** (`web/src`)
- `authConfig.ts` – MSAL config. Sign-in uses the auth code flow with PKCE.
  **Create account** sends `prompt=create`, which opens the sign-up page directly.
- `api.ts` – `useApi()` gets an access token for the API scope with `acquireTokenSilent`
  (refreshing as needed), falls back to a redirect if interaction is required, and sends it as
  `Authorization: Bearer <token>`.
- `redirect.html` / `redirect.ts` – MSAL v5's redirect bridge page; it's the registered
  redirect URI and hands auth responses back to the app.
- The landing page has a **Call API anonymously** button to show the API returning `401`.

## Entra setup

User **self-registration** needs a tenant that allows it. You have two options:

| | Entra **External ID** tenant (recommended) | Entra **workforce** tenant |
| --- | --- | --- |
| Who signs in | Customers / anyone who signs up | Your org's users (+ invited guests) |
| Self-service sign-up | ✅ via a "Sign up and sign in" user flow | ❌ ("Create account" won't work) |
| Authority | `https://<subdomain>.ciamlogin.com/` | `https://login.microsoftonline.com/<tenant-id>` |

The steps below are for External ID; differences for a workforce tenant are noted.

### 1. Create an External ID tenant

In the [Microsoft Entra admin center](https://entra.microsoft.com): **Entra ID → Overview → Manage
tenants → Create → External**. Note the tenant **subdomain** (e.g. `contosodemo` →
`contosodemo.ciamlogin.com`) and **Tenant ID**. Switch into the new tenant for the remaining steps.

### 2. Register the API

1. **App registrations → New registration**, name `entra-demo-api`, single tenant, no redirect URI.
2. Note its **Application (client) ID**.
3. **Expose an API** → set the **Application ID URI** (accept the default `api://<api-client-id>`).
4. **Add a scope**: name `access_as_user`, *Admins and users* can consent, fill in the display
   texts, enabled.
5. (Workforce tenants) In **Manifest**, set `"requestedAccessTokenVersion": 2`. External ID
   already issues v2 tokens.

### 3. Register the SPA

1. **App registrations → New registration**, name `entra-demo-web`.
2. Redirect URI: platform **Single-page application (SPA)**, URI
   `http://localhost:5173/redirect.html`.
3. Note its **Application (client) ID**.
4. **API permissions → Add a permission → APIs my organization uses → `entra-demo-api` →
   Delegated → `access_as_user`**, then **Grant admin consent**.

### 4. Enable sign-up (External ID only)

1. **External Identities → User flows → New user flow**, e.g. `SignUpSignIn`.
2. Pick identity providers (Email with password, or Email one-time passcode) and the attributes
   to collect at sign-up (e.g. *Display Name*).
3. Open the flow → **Applications → Add application** → select `entra-demo-web`.

### 5. Configure the API

Edit `api/EntraDemo.Api/appsettings.json` (or override with `appsettings.Development.json`,
user secrets, or environment variables like `AzureAd__ClientId`):

```jsonc
"AzureAd": {
  "Instance": "https://contosodemo.ciamlogin.com/",   // workforce: https://login.microsoftonline.com/
  "TenantId": "<tenant-id>",
  "ClientId": "<entra-demo-api client id>",
  "Scopes": "access_as_user"
},
"Cors": { "AllowedOrigins": [ "http://localhost:5173" ] }
```

### 6. Configure the SPA

```bash
cp web/.env.example web/.env.local
```

```ini
VITE_ENTRA_CLIENT_ID=<entra-demo-web client id>
VITE_ENTRA_AUTHORITY=https://contosodemo.ciamlogin.com/     # workforce: https://login.microsoftonline.com/<tenant-id>
VITE_API_SCOPE=api://<entra-demo-api client id>/access_as_user
VITE_API_BASE_URL=http://localhost:5016
```

If any value is missing, the app shows a setup screen listing what to fill in.

## Run it

Prerequisites: [.NET 8 SDK](https://dotnet.microsoft.com/download) and Node.js 20+.

```bash
# Terminal 1 – API on http://localhost:5016
cd api/EntraDemo.Api
dotnet run

# Terminal 2 – SPA on http://localhost:5173
cd web
npm install
npm run dev
```

Open http://localhost:5173, click **Create account** (or **Sign in**), and after returning you'll
see your identity as the API sees it plus a per-user todo list.

### Tests

```bash
cd api
dotnet test
```

The tests host the real API pipeline but swap Entra's signing keys for a local test key, then check:
no token → 401, malformed / expired / wrong-audience token → 401, token without
`access_as_user` → 403, valid token → 200, and users can't see or delete each other's todos.

### Calling the API by hand

`api/EntraDemo.Api/EntraDemo.Api.http` has ready-made requests (VS Code REST Client / Visual
Studio / Rider). To get a real token, sign in to the SPA, open DevTools → Network, and copy the
`Authorization` header from a request to `localhost:5016`.

## Troubleshooting

| Symptom | Likely cause |
| --- | --- |
| `AADSTS50011` redirect URI mismatch | SPA registration must have `http://localhost:5173/redirect.html` under the **SPA** platform (not Web). |
| "Create account" errors / shows sign-in page only | Tenant is a workforce tenant, or the app isn't added to a sign-up user flow. |
| `AADSTS65001` consent required | Grant admin consent for `access_as_user` on the SPA registration. |
| API returns `401 invalid_token` | `AzureAd:Instance` / `TenantId` / `ClientId` don't match the tenant and API app that issued the token. Check the API console log for the exact `IDX` error. |
| API returns `403` | Token lacks the `access_as_user` scope – check `VITE_API_SCOPE`. |
| Browser CORS error | Add the SPA origin to `Cors:AllowedOrigins`. |

## Going to production

- Serve both apps over HTTPS and register the production redirect URI
  (`https://your-app/redirect.html`); update `Cors:AllowedOrigins`.
- Replace the in-memory `TodoStore` with persistent storage.
- Consider app roles (`roles` claim) for admin-only endpoints, and `localStorage` cache if you
  want SSO across browser tabs.
