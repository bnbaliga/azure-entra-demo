import { InteractionStatus } from "@azure/msal-browser";
import { useMsal } from "@azure/msal-react";
import { signInRequest, signUpRequest } from "../authConfig";

export function SignInButton({ className = "button" }: { className?: string }) {
  const { instance, inProgress } = useMsal();
  return (
    <button
      className={className}
      disabled={inProgress !== InteractionStatus.None}
      onClick={() => instance.loginRedirect(signInRequest)}
    >
      Sign in
    </button>
  );
}

export function SignUpButton({ className = "button secondary" }: { className?: string }) {
  const { instance, inProgress } = useMsal();
  return (
    <button
      className={className}
      disabled={inProgress !== InteractionStatus.None}
      onClick={() => instance.loginRedirect(signUpRequest)}
    >
      Create account
    </button>
  );
}

export function SignOutButton() {
  const { instance } = useMsal();
  return (
    <button
      className="button secondary"
      onClick={() => instance.logoutRedirect({ account: instance.getActiveAccount() })}
    >
      Sign out
    </button>
  );
}
