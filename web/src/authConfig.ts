import { LogLevel, PromptValue, type Configuration, type RedirectRequest } from "@azure/msal-browser";

const env = import.meta.env;

export const settings = {
  clientId: env.VITE_ENTRA_CLIENT_ID ?? "",
  authority: env.VITE_ENTRA_AUTHORITY ?? "",
  apiScope: env.VITE_API_SCOPE ?? "",
  apiBaseUrl: (env.VITE_API_BASE_URL ?? "http://localhost:5016").replace(/\/$/, ""),
};

/** Names of required settings that are missing, so the UI can explain what to configure. */
export const missingSettings = (
  [
    ["VITE_ENTRA_CLIENT_ID", settings.clientId],
    ["VITE_ENTRA_AUTHORITY", settings.authority],
    ["VITE_API_SCOPE", settings.apiScope],
  ] as const
)
  .filter(([, value]) => !value || value.startsWith("00000000-") || value.includes("your-tenant"))
  .map(([name]) => name);

// Non-Microsoft-hosted authorities (e.g. External ID's *.ciamlogin.com) must be
// declared as known so MSAL trusts them without instance discovery.
function knownAuthorities(): string[] {
  try {
    const host = new URL(settings.authority).host;
    return host === "login.microsoftonline.com" ? [] : [host];
  } catch {
    return [];
  }
}

export const msalConfig: Configuration = {
  auth: {
    clientId: settings.clientId,
    authority: settings.authority,
    knownAuthorities: knownAuthorities(),
    // MSAL v5 returns auth responses through a small bridge page (see redirect.html).
    redirectUri: `${window.location.origin}/redirect.html`,
    postLogoutRedirectUri: window.location.origin,
  },
  cache: {
    // sessionStorage limits token lifetime to the browser tab; use localStorage for SSO across tabs.
    cacheLocation: "sessionStorage",
  },
  system: {
    loggerOptions: {
      logLevel: LogLevel.Warning,
      loggerCallback: (level, message, containsPii) => {
        if (containsPii) return;
        if (level === LogLevel.Error) console.error(message);
        else if (level === LogLevel.Warning) console.warn(message);
      },
    },
  },
};

/** Scopes requested for the API access token. openid/profile/offline_access are added by MSAL. */
export const apiScopes = [settings.apiScope];

export const signInRequest: RedirectRequest = {
  scopes: apiScopes,
};

/**
 * prompt=create takes the user straight to the sign-up page. This requires a tenant
 * with self-service sign-up enabled (Entra External ID with a "Sign up and sign in"
 * user flow linked to the app); workforce tenants reject it.
 */
export const signUpRequest: RedirectRequest = {
  scopes: apiScopes,
  prompt: PromptValue.CREATE,
};
