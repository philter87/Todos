using MediatR;
using Todos.Api.Data;
using Todos.Api.Features.Todos.GetTodos;

namespace Todos.Api.Features.Todos.UpdateTodo;

public class UpdateTodoHandler(AppDbContext db) : IRequestHandler<UpdateTodoCommand, TodoDto?>
{
    public async Task<TodoDto?> Handle(UpdateTodoCommand request, CancellationToken cancellationToken)
    {
        var todo = await db.Todos.FindAsync([request.Id], cancellationToken);
        if (todo is null)
            return null;

        todo.Title = request.Title;
        todo.Description = request.Description;
        todo.IsCompleted = request.IsCompleted;
        todo.UpdatedAt = DateTime.UtcNow;

        await db.SaveChangesAsync(cancellationToken);

        return new TodoDto(todo.Id, todo.Title, todo.Description, todo.IsCompleted, todo.CreatedAt, todo.UpdatedAt);
    }
}
