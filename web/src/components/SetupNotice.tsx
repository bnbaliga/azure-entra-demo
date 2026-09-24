export default function SetupNotice({ missing }: { missing: readonly string[] }) {
  return (
    <main className="container">
      <section className="card setup">
        <h1>Configuration needed</h1>
        <p>
          Copy <code>web/.env.example</code> to <code>web/.env.local</code>, fill in the values from
          your Entra app registrations, and restart <code>npm run dev</code>.
        </p>
        <p>Missing or placeholder values:</p>
        <ul>
          {missing.map((name) => (
            <li key={name}>
              <code>{name}</code>
            </li>
          ))}
        </ul>
        <p className="muted">See the README for step-by-step Entra setup.</p>
      </section>
    </main>
  );
}
