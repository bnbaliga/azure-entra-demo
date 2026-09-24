import { StrictMode } from "react";
import { createRoot } from "react-dom/client";
import { EventType, PublicClientApplication, type AuthenticationResult } from "@azure/msal-browser";
import { MsalProvider } from "@azure/msal-react";
import { missingSettings, msalConfig } from "./authConfig";
import App from "./App";
import SetupNotice from "./components/SetupNotice";
import "./styles.css";

const root = createRoot(document.getElementById("root")!);

async function start() {
  if (missingSettings.length > 0) {
    root.render(<SetupNotice missing={missingSettings} />);
    return;
  }

  const msalInstance = new PublicClientApplication(msalConfig);
  await msalInstance.initialize();

  // Complete a sign-in/sign-up redirect (if one is in flight) before rendering,
  // so components never see "authenticated" without an active account.
  const redirectResult = await msalInstance.handleRedirectPromise();
  if (redirectResult?.account) {
    msalInstance.setActiveAccount(redirectResult.account);
  }

  // Restore the signed-in account after a page reload.
  if (!msalInstance.getActiveAccount()) {
    const [first] = msalInstance.getAllAccounts();
    if (first) msalInstance.setActiveAccount(first);
  }

  // After a successful sign-in or sign-up, make that account the active one.
  msalInstance.addEventCallback((event) => {
    if (
      (event.eventType === EventType.LOGIN_SUCCESS ||
        event.eventType === EventType.ACQUIRE_TOKEN_SUCCESS) &&
      event.payload
    ) {
      msalInstance.setActiveAccount((event.payload as AuthenticationResult).account);
    }
  });

  root.render(
    <StrictMode>
      <MsalProvider instance={msalInstance}>
        <App />
      </MsalProvider>
    </StrictMode>,
  );
}

start().catch((error: unknown) => {
  console.error(error);
  root.render(<pre className="error">Failed to start: {String(error)}</pre>);
});
