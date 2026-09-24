import { useCallback, useEffect, useState, type FormEvent } from "react";
import { useApi, type Todo } from "../api";

export default function TodosPanel() {
  const api = useApi();
  const [todos, setTodos] = useState<Todo[]>([]);
  const [title, setTitle] = useState("");
  const [error, setError] = useState<string>();
  const [loading, setLoading] = useState(true);

  const run = useCallback(async (action: () => Promise<void>) => {
    setError(undefined);
    try {
      await action();
    } catch (e) {
      setError(e instanceof Error ? e.message : String(e));
    }
  }, []);

  useEffect(() => {
    run(async () => setTodos(await api<Todo[]>("/api/todos"))).finally(() => setLoading(false));
  }, [api, run]);

  const add = (e: FormEvent) => {
    e.preventDefault();
    const trimmed = title.trim();
    if (!trimmed) return;
    run(async () => {
      const created = await api<Todo>("/api/todos", {
        method: "POST",
        body: JSON.stringify({ title: trimmed }),
      });
      setTodos((current) => [...current, created]);
      setTitle("");
    });
  };

  const toggle = (todo: Todo) =>
    run(async () => {
      const updated = await api<Todo>(`/api/todos/${todo.id}`, {
        method: "PATCH",
        body: JSON.stringify({ done: !todo.done }),
      });
      setTodos((current) => current.map((t) => (t.id === updated.id ? updated : t)));
    });

  const remove = (todo: Todo) =>
    run(async () => {
      await api<void>(`/api/todos/${todo.id}`, { method: "DELETE" });
      setTodos((current) => current.filter((t) => t.id !== todo.id));
    });

  return (
    <section className="card">
      <h2>My todos</h2>
      <p className="muted">
        Stored by the API per user (keyed on your token's <code>oid</code> claim).
      </p>
      <form className="todo-form" onSubmit={add}>
        <input
          value={title}
          onChange={(e) => setTitle(e.target.value)}
          placeholder="What needs doing?"
          aria-label="New todo"
        />
        <button className="button" type="submit" disabled={!title.trim()}>
          Add
        </button>
      </form>
      {error && <p className="error">{error}</p>}
      {loading ? (
        <p className="muted">Loading…</p>
      ) : todos.length === 0 ? (
        !error && <p className="muted">Nothing yet.</p>
      ) : (
        <ul className="todos">
          {todos.map((todo) => (
            <li key={todo.id} className={todo.done ? "done" : undefined}>
              <label>
                <input type="checkbox" checked={todo.done} onChange={() => toggle(todo)} />
                <span>{todo.title}</span>
              </label>
              <button className="link danger" onClick={() => remove(todo)} aria-label="Delete">
                Delete
              </button>
            </li>
          ))}
        </ul>
      )}
    </section>
  );
}
