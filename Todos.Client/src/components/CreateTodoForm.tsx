import { useState } from 'react';
import { todosApi, type CreateTodoRequest } from '../api/todosApi';

interface Props {
  onCreated: () => void;
}

export function CreateTodoForm({ onCreated }: Props) {
  const [title, setTitle] = useState('');
  const [description, setDescription] = useState('');
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  async function handleSubmit(e: React.FormEvent) {
    e.preventDefault();
    if (!title.trim()) return;

    const request: CreateTodoRequest = {
      title: title.trim(),
      description: description.trim() || undefined,
    };

    setLoading(true);
    setError(null);
    try {
      await todosApi.create(request);
      setTitle('');
      setDescription('');
      onCreated();
    } catch {
      setError('Failed to create todo. Is the API running?');
    } finally {
      setLoading(false);
    }
  }

  return (
    <form className="create-form" onSubmit={handleSubmit}>
      <h2>New Todo</h2>
      {error && <p className="error">{error}</p>}
      <div className="form-group">
        <input
          type="text"
          placeholder="Title *"
          value={title}
          onChange={(e) => setTitle(e.target.value)}
          required
          disabled={loading}
        />
      </div>
      <div className="form-group">
        <textarea
          placeholder="Description (optional)"
          value={description}
          onChange={(e) => setDescription(e.target.value)}
          rows={2}
          disabled={loading}
        />
      </div>
      <button type="submit" disabled={loading || !title.trim()}>
        {loading ? 'Adding…' : 'Add Todo'}
      </button>
    </form>
  );
}
