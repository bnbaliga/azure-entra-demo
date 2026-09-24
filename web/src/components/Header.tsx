import { AuthenticatedTemplate, UnauthenticatedTemplate, useMsal } from "@azure/msal-react";
import { SignInButton, SignOutButton, SignUpButton } from "./AuthButtons";

export default function Header() {
  const { instance } = useMsal();
  const account = instance.getActiveAccount();

  return (
    <header className="header">
      <div className="container header-inner">
        <span className="brand">Entra ID Demo</span>
        <nav className="actions">
          <AuthenticatedTemplate>
            <span className="muted">{account?.name ?? account?.username}</span>
            <SignOutButton />
          </AuthenticatedTemplate>
          <UnauthenticatedTemplate>
            <SignUpButton />
            <SignInButton />
          </UnauthenticatedTemplate>
        </nav>
      </div>
    </header>
  );
}
