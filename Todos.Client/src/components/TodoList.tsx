import { useCallback, useEffect, useState } from 'react';
import { type Todo, todosApi } from '../api/todosApi';
import { TodoItem } from './TodoItem';

interface Props {
  refresh: number;
  onRefreshed: () => void;
}

export function TodoList({ refresh, onRefreshed }: Props) {
  const [todos, setTodos] = useState<Todo[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  const load = useCallback(async () => {
    setLoading(true);
    setError(null);
    try {
      const data = await todosApi.getAll();
      setTodos(data);
    } catch {
      setError('Could not load todos. Is the API running on port 5157?');
    } finally {
      setLoading(false);
    }
  }, []);

  useEffect(() => {
    load().then(onRefreshed);
  }, [load, refresh, onRefreshed]);

  if (loading) return <p className="status-msg">Loading…</p>;
  if (error) return <p className="error">{error}</p>;
  if (todos.length === 0) return <p className="status-msg">No todos yet. Add one above!</p>;

  const pending = todos.filter((t) => !t.isCompleted);
  const done = todos.filter((t) => t.isCompleted);

  return (
    <div className="todo-list">
      {pending.length > 0 && (
        <section>
          <h3 className="list-heading">Pending ({pending.length})</h3>
          {pending.map((todo) => (
            <TodoItem key={todo.id} todo={todo} onUpdated={load} />
          ))}
        </section>
      )}
      {done.length > 0 && (
        <section>
          <h3 className="list-heading completed-heading">Completed ({done.length})</h3>
          {done.map((todo) => (
            <TodoItem key={todo.id} todo={todo} onUpdated={load} />
          ))}
        </section>
      )}
    </div>
  );
}
