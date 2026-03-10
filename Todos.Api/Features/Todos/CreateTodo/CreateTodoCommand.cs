using MediatR;
using Todos.Api.Features.Todos.GetTodos;

namespace Todos.Api.Features.Todos.CreateTodo;

public record CreateTodoCommand(string Title, string? Description) : IRequest<TodoDto>;
