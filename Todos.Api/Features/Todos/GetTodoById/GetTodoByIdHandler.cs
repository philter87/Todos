using MediatR;
using Microsoft.EntityFrameworkCore;
using Todos.Api.Data;
using Todos.Api.Features.Todos.GetTodos;

namespace Todos.Api.Features.Todos.GetTodoById;

public class GetTodoByIdHandler(AppDbContext db) : IRequestHandler<GetTodoByIdQuery, TodoDto?>
{
    public async Task<TodoDto?> Handle(GetTodoByIdQuery request, CancellationToken cancellationToken) =>
        await db.Todos
            .Where(t => t.Id == request.Id)
            .Select(t => new TodoDto(t.Id, t.Title, t.Description, t.IsCompleted, t.CreatedAt, t.UpdatedAt))
            .FirstOrDefaultAsync(cancellationToken);
}
