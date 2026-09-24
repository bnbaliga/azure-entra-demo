import { useCallback } from "react";
import { InteractionRequiredAuthError } from "@azure/msal-browser";
import { useMsal } from "@azure/msal-react";
import { apiScopes, settings } from "./authConfig";

export class ApiError extends Error {
  constructor(
    public readonly status: number,
    message: string,
  ) {
    super(message);
  }
}

export interface Me {
  name: string | null;
  username: string | null;
  objectId: string | null;
  tenantId: string | null;
  scopes: string[];
  claims: { type: string; value: string }[];
}

export interface Todo {
  id: string;
  title: string;
  done: boolean;
  createdAt: string;
}

async function send<T>(path: string, init: RequestInit = {}, accessToken?: string): Promise<T> {
  const headers = new Headers(init.headers);
  if (init.body) headers.set("Content-Type", "application/json");
  if (accessToken) headers.set("Authorization", `Bearer ${accessToken}`);

  const response = await fetch(`${settings.apiBaseUrl}${path}`, { ...init, headers });
  if (!response.ok) {
    const challenge = response.headers.get("WWW-Authenticate");
    const detail = challenge ?? ((await response.text()) || response.statusText);
    throw new ApiError(response.status, `${response.status} ${response.statusText}: ${detail}`);
  }
  return (response.status === 204 ? undefined : await response.json()) as T;
}

/**
 * Returns a fetch helper that attaches an Entra access token for the API.
 * Tokens come from MSAL's cache and are refreshed silently; if the user must
 * interact (consent, MFA, expired session) we fall back to a redirect.
 */
export function useApi() {
  const { instance } = useMsal();

  return useCallback(
    async <T>(path: string, init?: RequestInit): Promise<T> => {
      const account = instance.getActiveAccount();
      if (!account) throw new Error("Not signed in.");

      let accessToken: string;
      try {
        ({ accessToken } = await instance.acquireTokenSilent({ scopes: apiScopes, account }));
      } catch (error) {
        if (error instanceof InteractionRequiredAuthError) {
          await instance.acquireTokenRedirect({ scopes: apiScopes, account });
        }
        throw error;
      }

      return send<T>(path, init, accessToken);
    },
    [instance],
  );
}

/** Calls the API with no Authorization header, to demonstrate that it is rejected. */
export const callWithoutToken = <T>(path: string) => send<T>(path);
