import { useCallback, useState } from 'react';
import './App.css';
import { CreateTodoForm } from './components/CreateTodoForm';
import { TodoList } from './components/TodoList';

function App() {
  const [refreshKey, setRefreshKey] = useState(0);

  const triggerRefresh = useCallback(() => setRefreshKey((k) => k + 1), []);
  const noop = useCallback(() => {}, []);

  return (
    <div className="app">
      <header className="app-header">
        <h1>✅ Todos</h1>
      </header>
      <main className="app-main">
        <CreateTodoForm onCreated={triggerRefresh} />
        <TodoList refresh={refreshKey} onRefreshed={noop} />
      </main>
    </div>
  );
}

export default App;
