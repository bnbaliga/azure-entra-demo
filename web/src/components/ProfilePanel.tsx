import { useEffect, useState } from "react";
import { useApi, type Me } from "../api";

export default function ProfilePanel() {
  const api = useApi();
  const [me, setMe] = useState<Me>();
  const [error, setError] = useState<string>();
  const [showClaims, setShowClaims] = useState(false);

  useEffect(() => {
    api<Me>("/api/me")
      .then(setMe)
      .catch((e: unknown) => setError(e instanceof Error ? e.message : String(e)));
  }, [api]);

  return (
    <section className="card">
      <h2>Your identity, as seen by the API</h2>
      <p className="muted">
        Returned by <code>GET /api/me</code> after validating your access token.
      </p>
      {error && <p className="error">{error}</p>}
      {!me && !error && <p className="muted">Loading…</p>}
      {me && (
        <>
          <dl className="details">
            <dt>Name</dt>
            <dd>{me.name ?? "—"}</dd>
            <dt>Username</dt>
            <dd>{me.username ?? "—"}</dd>
            <dt>Object ID</dt>
            <dd>
              <code>{me.objectId}</code>
            </dd>
            <dt>Tenant ID</dt>
            <dd>
              <code>{me.tenantId}</code>
            </dd>
            <dt>Scopes</dt>
            <dd>{me.scopes.join(", ")}</dd>
          </dl>
          <button className="link" onClick={() => setShowClaims((s) => !s)}>
            {showClaims ? "Hide" : "Show"} all token claims
          </button>
          {showClaims && (
            <table className="claims">
              <tbody>
                {me.claims.map((c, i) => (
                  <tr key={i}>
                    <th>{c.type}</th>
                    <td>{c.value}</td>
                  </tr>
                ))}
              </tbody>
            </table>
          )}
        </>
      )}
    </section>
  );
}
