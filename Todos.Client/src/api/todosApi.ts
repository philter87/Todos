const BASE_URL = 'http://localhost:5157/api/todos';

export interface Todo {
  id: number;
  title: string;
  description: string | null;
  isCompleted: boolean;
  createdAt: string;
  updatedAt: string | null;
}

export interface CreateTodoRequest {
  title: string;
  description?: string;
}

export interface UpdateTodoRequest {
  title: string;
  description?: string;
  isCompleted: boolean;
}

async function handleResponse<T>(res: Response): Promise<T> {
  if (!res.ok) {
    throw new Error(`Request failed: ${res.status} ${res.statusText}`);
  }
  return res.json() as Promise<T>;
}

export const todosApi = {
  getAll: (): Promise<Todo[]> =>
    fetch(BASE_URL).then((r) => handleResponse<Todo[]>(r)),

  getById: (id: number): Promise<Todo> =>
    fetch(`${BASE_URL}/${id}`).then((r) => handleResponse<Todo>(r)),

  create: (data: CreateTodoRequest): Promise<Todo> =>
    fetch(BASE_URL, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(data),
    }).then((r) => handleResponse<Todo>(r)),

  update: (id: number, data: UpdateTodoRequest): Promise<Todo> =>
    fetch(`${BASE_URL}/${id}`, {
      method: 'PUT',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(data),
    }).then((r) => handleResponse<Todo>(r)),
};
