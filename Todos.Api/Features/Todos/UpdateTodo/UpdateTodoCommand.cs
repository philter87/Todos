using MediatR;
using Todos.Api.Features.Todos.GetTodos;

namespace Todos.Api.Features.Todos.UpdateTodo;

public record UpdateTodoCommand(int Id, string Title, string? Description, bool IsCompleted) : IRequest<TodoDto?>;
