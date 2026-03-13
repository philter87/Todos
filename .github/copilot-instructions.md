# Project Guidelines

## Code Style

- Follow the existing patterns in each project instead of introducing new abstractions.
- In `Todos.Api`, keep request handling asynchronous end to end and pass `CancellationToken` through controller actions, MediatR handlers, and EF Core calls.
- In `Todos.Client`, use functional React components with local state and the existing fetch-based API layer in `src/api/todosApi.ts`.
- In `Todos.Tests`, prefer the existing `Any` helpers for request data and add integration-style tests through `TodosApiFactory` rather than mocking the API surface.

## Architecture

- `Todos.Api` uses feature folders under `Features/Todos/`; keep controllers thin and put endpoint behavior in MediatR query or command handlers.
- Return `TodoDto` values from handlers and project EF Core entities to DTOs inside queries instead of exposing entities directly.
- `Todos.Client` is a small React frontend with component-local state; keep API calls centralized in `src/api/todosApi.ts` and UI concerns in `src/components/`.
- `Todos.Tests` exercises the real API through `WebApplicationFactory<Program>` with SQLite in memory.

## Build and Test

- Restore .NET dependencies from the repository root with `dotnet restore .\Todos.slnx`.
- Run API tests from the repository root with `dotnet test .\Todos.slnx`.
- In `Todos.Client`, install dependencies with `npm install`, run the dev server with `npm run dev`, lint with `npm run lint`, and build with `npm run build`.
- Use the root `README.md` for current local setup details and default development URLs.

## Conventions

- The API uses SQLite and calls `EnsureCreated()` on startup; do not introduce migrations or alternate database setup unless the task requires it.
- The client currently depends on the API being available at `http://localhost:5157/api/todos`; if you change client-server integration, update both sides intentionally.
- CORS is configured for the Vite dev server at `http://localhost:5173`; preserve that workflow unless the task explicitly changes local hosting.
- The `Tutorial/` folder is exercise material and usually not part of the application runtime or test flow unless a task explicitly targets it.