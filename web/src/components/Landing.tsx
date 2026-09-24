import { useState } from "react";
import { callWithoutToken } from "../api";
import { SignInButton, SignUpButton } from "./AuthButtons";

export default function Landing() {
  const [result, setResult] = useState<string>();

  const probe = async () => {
    setResult("Calling GET /api/me without a token…");
    try {
      await callWithoutToken("/api/me");
      setResult("Unexpected: the API accepted an anonymous call.");
    } catch (error) {
      setResult(`Rejected as expected → ${error instanceof Error ? error.message : String(error)}`);
    }
  };

  return (
    <section className="hero">
      <h1>Sign in with Microsoft Entra ID</h1>
      <p className="muted">
        Sign in with an existing account, or create one. The app then calls a .NET API that only
        accepts requests carrying a valid Entra access token.
      </p>
      <div className="actions">
        <SignInButton className="button large" />
        <SignUpButton className="button secondary large" />
      </div>

      <div className="card probe">
        <h2>Is the API really protected?</h2>
        <p className="muted">Call it without signing in and see what happens.</p>
        <button className="button secondary" onClick={probe}>
          Call API anonymously
        </button>
        {result && <pre className="output">{result}</pre>}
      </div>
    </section>
  );
}
