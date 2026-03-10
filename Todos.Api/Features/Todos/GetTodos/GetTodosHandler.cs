using MediatR;
using Microsoft.EntityFrameworkCore;
using Todos.Api.Data;

namespace Todos.Api.Features.Todos.GetTodos;

public class GetTodosHandler(AppDbContext db) : IRequestHandler<GetTodosQuery, List<TodoDto>>
{
    public async Task<List<TodoDto>> Handle(GetTodosQuery request, CancellationToken cancellationToken) =>
        await db.Todos
            .OrderByDescending(t => t.CreatedAt)
            .Select(t => new TodoDto(t.Id, t.Title, t.Description, t.IsCompleted, t.CreatedAt, t.UpdatedAt))
            .ToListAsync(cancellationToken);
}
