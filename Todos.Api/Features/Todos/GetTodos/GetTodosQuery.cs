using MediatR;
using Todos.Api.Features.Todos.GetTodos;

namespace Todos.Api.Features.Todos.GetTodos;

public record GetTodosQuery : IRequest<List<TodoDto>>;
