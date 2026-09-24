import { broadcastResponseToMainFrame } from "@azure/msal-browser/redirect-bridge";

broadcastResponseToMainFrame().catch((error: unknown) => {
  console.error("Failed to process the sign-in response", error);
  document.body.textContent = "Sign-in failed. Close this window and try again.";
});
