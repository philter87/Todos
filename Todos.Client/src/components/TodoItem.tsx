import { useState } from 'react';
import { type Todo, todosApi } from '../api/todosApi';
import { EditTodoModal } from './EditTodoModal';

interface Props {
  todo: Todo;
  onUpdated: () => void;
}

export function TodoItem({ todo, onUpdated }: Props) {
  const [editing, setEditing] = useState(false);
  const [toggling, setToggling] = useState(false);

  async function handleToggle() {
    setToggling(true);
    try {
      await todosApi.update(todo.id, {
        title: todo.title,
        description: todo.description ?? undefined,
        isCompleted: !todo.isCompleted,
      });
      onUpdated();
    } finally {
      setToggling(false);
    }
  }

  const createdDate = new Date(todo.createdAt).toLocaleDateString();

  return (
    <>
      <div className={`todo-item ${todo.isCompleted ? 'completed' : ''}`}>
        <button
          className={`toggle-btn ${todo.isCompleted ? 'checked' : ''}`}
          onClick={handleToggle}
          disabled={toggling}
          aria-label={todo.isCompleted ? 'Mark incomplete' : 'Mark complete'}
        >
          {todo.isCompleted ? '✓' : ''}
        </button>
        <div className="todo-content">
          <span className="todo-title">{todo.title}</span>
          {todo.description && (
            <span className="todo-description">{todo.description}</span>
          )}
          <span className="todo-date">Created {createdDate}</span>
        </div>
        <button
          className="edit-btn"
          onClick={() => setEditing(true)}
          aria-label="Edit todo"
        >
          ✎
        </button>
      </div>
      {editing && (
        <EditTodoModal
          todo={todo}
          onClose={() => setEditing(false)}
          onUpdated={onUpdated}
        />
      )}
    </>
  );
}
