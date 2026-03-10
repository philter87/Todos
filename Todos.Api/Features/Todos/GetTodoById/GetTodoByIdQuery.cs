using MediatR;
using Todos.Api.Features.Todos.GetTodos;

namespace Todos.Api.Features.Todos.GetTodoById;

public record GetTodoByIdQuery(int Id) : IRequest<TodoDto?>;
