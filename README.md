# Todos

Small full-stack todo application with an ASP.NET Core API, a React client, and integration tests.

## Stack

- API: ASP.NET Core, MediatR, Entity Framework Core, SQLite
- Client: React, TypeScript, Vite
- Tests: xUnit with `WebApplicationFactory`

## Repository Layout

- `Todos.Api` - REST API and SQLite-backed persistence
- `Todos.Client` - React frontend for creating, listing, and updating todos
- `Todos.Tests` - integration tests for the API
- `Tutorial` - exercise material and notes

## Prerequisites

- .NET 10 SDK
- Node.js 20+ and npm

## Getting Started

### 1. Restore dependencies

From the repository root:

```powershell
dotnet restore .\Todos.slnx
cd .\Todos.Client
npm install
```

### 2. Start the API

In one terminal:

```powershell
cd .\Todos.Api
dotnet run
```

Default local URLs:

- `http://localhost:5157`
- `https://localhost:7012`
- Swagger UI: `http://localhost:5157/swagger`

The API creates a local SQLite database on startup with `EnsureCreated()`.

### 3. Start the client

In a second terminal:

```powershell
cd .\Todos.Client
npm run dev
```

The Vite dev server runs on `http://localhost:5173`.

## Running Tests

From the repository root:

```powershell
dotnet test .\Todos.slnx
```

The test project boots the API in memory and uses SQLite for integration-style endpoint tests.

## API Summary

Base route: `http://localhost:5157/api/todos`

- `GET /api/todos` - list all todos
- `GET /api/todos/{id}` - get a single todo
- `POST /api/todos` - create a todo
- `PUT /api/todos/{id}` - update a todo

Todo shape:

```json
{
  "id": 1,
  "title": "Buy milk",
  "description": "From the supermarket",
  "isCompleted": false,
  "createdAt": "2026-03-13T12:00:00Z",
  "updatedAt": null
}
```

## Notes

- The frontend API base URL is currently hardcoded in `Todos.Client/src/api/todosApi.ts` to `http://localhost:5157/api/todos`.
- CORS is configured for the Vite dev server at `http://localhost:5173`.
- The client README is still the default Vite template and can be ignored in favor of this root README.