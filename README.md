# React / ASP.NET MVC + .NET API secured with Microsoft Entra ID

A demo of front ends that sign users in (or let them create an account) with
**Microsoft Entra ID** using OpenID Connect, and a **.NET 8 Web API** that only accepts
calls carrying a valid Entra-issued OAuth 2.0 access token.

There are two front ends that do the same thing. Use either one, or both:

| | React SPA (`web/`) | ASP.NET Core MVC (`mvc/`) |
| --- | --- | --- |
| OAuth client type | Public client (no secret) | Confidential client (client secret) |
| Who holds the tokens | The browser (MSAL cache in `sessionStorage`) | The server (MSAL token cache). The browser only gets an auth cookie |
| Who calls the API | Browser → API (needs CORS) | MVC server → API (no CORS needed) |
| Entra platform type | Single-page application | Web |
| URL | http://localhost:5173 | https://localhost:7180 |

### React SPA flow

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

### MVC flow

```
┌─────────┐ 1. GET /Dashboard  ┌──────────────────────┐ 2. OIDC redirect (auth code)    ┌──────────────────┐
│ Browser │ ─────────────────▶ │ ASP.NET MVC (mvc/)   │ ──────────────────────────────▶ │ Microsoft Entra  │
│         │ ◀── HTML + cookie ─│ Microsoft.Identity   │ ◀─ 3. code → tokens (secret) ── │ ID / External ID │
└─────────┘                    │ .Web · :7180         │                                 └──────────────────┘
                               └─────────┬────────────┘
                                         │ 4. Authorization: Bearer <access token>  (server → server)
                                         ▼
                               ┌──────────────────────┐
                               │  .NET API (api/)     │  same validation as above
                               └──────────────────────┘
```

## What's in here

| Path | What it is |
| --- | --- |
| `web/` | React 19 + Vite + TypeScript SPA using `@azure/msal-react` / `@azure/msal-browser` v5 |
| `mvc/EntraDemo.Mvc/` | ASP.NET Core 8 MVC app using `Microsoft.Identity.Web` for OIDC sign-in and token acquisition |
| `mvc/EntraDemo.Mvc.Tests/` | Integration tests for the MVC app's sign-in redirects, token forwarding and antiforgery |
| `api/EntraDemo.Api/` | ASP.NET Core 8 minimal API using `Microsoft.Identity.Web` for JWT bearer validation |
| `api/EntraDemo.Api.Tests/` | Integration tests showing that calls with no token, a bad token or a missing scope are rejected |
| `EntraDemo.sln` | Solution containing all the .NET projects |

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
only sees their own. That is true whichever front end they use. Swap `TodoStore` for a database
for anything real.

### How the security works

**API** (`api/EntraDemo.Api/Program.cs`)
- `AddMicrosoftIdentityWebApi` validates the bearer token's signature (keys from the tenant's
  OIDC metadata), issuer, audience (`<client-id>` or `api://<client-id>`) and lifetime.
- A **fallback authorization policy** requires an authenticated user holding the
  `access_as_user` scope on *every* endpoint unless it opts out with `AllowAnonymous()`.
  New endpoints are secure by default.
- CORS allows only the SPA's origin. The MVC app calls the API from the server, so it needs no CORS.

**React SPA** (`web/src`)
- `authConfig.ts`: the MSAL config. Sign-in uses the auth code flow with PKCE.
  **Create account** sends `prompt=create`, which opens the sign-up page directly.
- `api.ts`: `useApi()` gets an access token for the API scope with `acquireTokenSilent`
  (refreshing as needed), falls back to a redirect if interaction is required, and sends it as
  `Authorization: Bearer <token>`.
- `redirect.html` / `redirect.ts`: MSAL v5's redirect bridge page. It's the registered
  redirect URI and hands auth responses back to the app.

**MVC** (`mvc/EntraDemo.Mvc`)
- `Program.cs`: `AddMicrosoftIdentityWebApp` sets up OIDC sign-in with a cookie session;
  `EnableTokenAcquisitionToCallDownstreamApi` requests the API scope at sign-in and caches the
  tokens server-side. A global `AuthorizeFilter` makes every page require sign-in unless it is
  marked `[AllowAnonymous]`.
- `Controllers/AccountController.cs`: **SignIn** starts the OIDC challenge, **SignUp** starts it with
  `prompt=create`, and **SignOut** (a POST with an antiforgery token) clears the cookie and signs
  out of Entra.
- `Services/TodoApiClient.cs`: a typed `HttpClient`. `IAuthorizationHeaderProvider` returns
  `Bearer <token>` for the signed-in user from the token cache, refreshing it if needed.
- `Controllers/DashboardController.cs`: `[AuthorizeForScopes]` turns "user must sign in again"
  token errors (for example, an empty token cache after an app restart) into a sign-in redirect.
  Todo changes are form POSTs with antiforgery tokens, followed by a redirect back to the dashboard.

Both front ends have a **Call API anonymously** button on the landing page that shows the API returning `401`.

## Entra setup

User **self-registration** needs a tenant that allows it. You have two options:

| | Entra **External ID** tenant (recommended) | Entra **workforce** tenant |
| --- | --- | --- |
| Who signs in | Customers / anyone who signs up | Your org's users (+ invited guests) |
| Self-service sign-up | ✅ via a "Sign up and sign in" user flow | ❌ ("Create account" won't work) |
| Authority | `https://<subdomain>.ciamlogin.com/` | `https://login.microsoftonline.com/<tenant-id>` |

The steps below are for External ID; differences for a workforce tenant are noted. Skip step 3
or 4 if you only want one of the front ends.

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

### 3. Register the React SPA

1. **App registrations → New registration**, name `entra-demo-web`.
2. Redirect URI: platform **Single-page application (SPA)**, URI
   `http://localhost:5173/redirect.html`.
3. Note its **Application (client) ID**.
4. **API permissions → Add a permission → APIs my organization uses → `entra-demo-api` →
   Delegated → `access_as_user`**, then **Grant admin consent**.

### 4. Register the MVC app

1. **App registrations → New registration**, name `entra-demo-mvc`.
2. Redirect URI: platform **Web**, URI `https://localhost:7180/signin-oidc`.
3. **Authentication**: set **Front-channel logout URL** to `https://localhost:7180/signout-oidc`,
   and under the Web platform add `https://localhost:7180/signout-callback-oidc` as a redirect
   URI (used after sign-out).
4. Note its **Application (client) ID**.
5. **Certificates & secrets → New client secret**. Copy the **Value** now; it's only shown once.
6. **API permissions → Add a permission → APIs my organization uses → `entra-demo-api` →
   Delegated → `access_as_user`**, then **Grant admin consent**.

### 5. Enable sign-up (External ID only)

1. **External Identities → User flows → New user flow**, e.g. `SignUpSignIn`.
2. Pick identity providers (Email with password, or Email one-time passcode) and the attributes
   to collect at sign-up (e.g. *Display Name*).
3. Open the flow → **Applications → Add application** → add `entra-demo-web` and/or
   `entra-demo-mvc`.

### 6. Configure the API

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

### 7a. Configure the React SPA

```bash
cp web/.env.example web/.env.local
```

```ini
VITE_ENTRA_CLIENT_ID=<entra-demo-web client id>
VITE_ENTRA_AUTHORITY=https://contosodemo.ciamlogin.com/     # workforce: https://login.microsoftonline.com/<tenant-id>
VITE_API_SCOPE=api://<entra-demo-api client id>/access_as_user
VITE_API_BASE_URL=http://localhost:5016
```

### 7b. Configure the MVC app

Edit `mvc/EntraDemo.Mvc/appsettings.json`:

```jsonc
"AzureAd": {
  "Instance": "https://contosodemo.ciamlogin.com/",   // workforce: https://login.microsoftonline.com/
  "TenantId": "<tenant-id>",
  "ClientId": "<entra-demo-mvc client id>",
  ...
},
"TodoApi": {
  "BaseUrl": "http://localhost:5016",
  "Scopes": [ "api://<entra-demo-api client id>/access_as_user" ]
}
```

Keep the client secret out of source control by storing it in user secrets:

```bash
cd mvc/EntraDemo.Mvc
dotnet user-secrets set "AzureAd:ClientSecret" "<client secret value>"
```

Both front ends show a "Configuration needed" notice that lists any settings still missing.

## Run it

Prerequisites: [.NET 8 SDK](https://dotnet.microsoft.com/download) and Node.js 20+ (Node.js is only needed for the React app).

```bash
# Terminal 1: API on http://localhost:5016
cd api/EntraDemo.Api
dotnet run

# Terminal 2a: React SPA on http://localhost:5173
cd web
npm install
npm run dev

# Terminal 2b: MVC app on https://localhost:7180
dotnet dev-certs https --trust   # first time only
cd mvc/EntraDemo.Mvc
dotnet run
```

Open either front end and click **Create account** (or **Sign in**). When you come back,
you'll see your identity as the API sees it and your own todo list. Both front ends talk to
the same API, so a todo added in one shows up in the other for the same user.

### Tests

```bash
dotnet test            # from the repo root: runs the API and MVC test projects
cd web && npm run build   # type-checks and builds the SPA
```

- **API tests** host the real API pipeline but swap Entra's signing keys for a local test key.
  They check these cases:
  - no token → 401
  - malformed, expired or wrong-audience token → 401
  - token without `access_as_user` → 403
  - valid token → 200
  - users can't see or delete each other's todos
- **MVC tests** host the real MVC app with static OIDC metadata, a fake token provider and a stub API.
  They check these cases:
  - anonymous users are sent to Entra's authorize endpoint (auth code, API scope requested)
  - **Create account** sends `prompt=create`
  - every API call carries the `Bearer` token
  - form POSTs require antiforgery tokens
  - API errors are shown on the page instead of crashing it

### Calling the API by hand

`api/EntraDemo.Api/EntraDemo.Api.http` has ready-made requests (VS Code REST Client / Visual
Studio / Rider). To get a real token, sign in to the SPA, open DevTools → Network, and copy the
`Authorization` header from a request to `localhost:5016`.

## Troubleshooting

| Symptom | Likely cause |
| --- | --- |
| `AADSTS50011` redirect URI mismatch | SPA: `http://localhost:5173/redirect.html` must be under the **SPA** platform. MVC: `https://localhost:7180/signin-oidc` must be under the **Web** platform. |
| `AADSTS7000215` invalid client secret (MVC) | The secret is wrong or expired, or you copied the secret *ID* instead of its *Value*. |
| "Create account" errors / shows sign-in page only | Tenant is a workforce tenant, or the app isn't added to a sign-up user flow. |
| `AADSTS65001` consent required | Grant admin consent for `access_as_user` on the front end's registration. |
| API returns `401 invalid_token` | `AzureAd:Instance` / `TenantId` / `ClientId` don't match the tenant and API app that issued the token. Check the API console log for the exact `IDX` error. |
| API returns `403` | Token lacks the `access_as_user` scope. Check `VITE_API_SCOPE` / `TodoApi:Scopes`. |
| Browser CORS error (SPA) | Add the SPA origin to `Cors:AllowedOrigins`. |
| MVC: sign-in loops or "Correlation failed" | Use the HTTPS URL (`https://localhost:7180`) and trust the dev cert. OIDC cookies require HTTPS. |
| MVC: redirected to sign-in after restarting the app | Expected. The in-memory token cache was cleared, so `[AuthorizeForScopes]` signs you in again. |

## Going to production

- Serve everything over HTTPS and register the production redirect URIs
  (`https://your-spa/redirect.html`, `https://your-mvc/signin-oidc`); update `Cors:AllowedOrigins`.
- MVC: use a certificate or managed identity instead of a client secret (Key Vault), and a
  distributed token cache (`AddDistributedTokenCaches` with Redis or SQL) when running more than one instance.
- Replace the in-memory `TodoStore` with persistent storage.
- Consider app roles (`roles` claim) for admin-only endpoints.
