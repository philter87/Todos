using MediatR;
using Todos.Api.Data;
using Todos.Api.Features.Todos.GetTodos;

namespace Todos.Api.Features.Todos.CreateTodo;

public class CreateTodoHandler(AppDbContext db) : IRequestHandler<CreateTodoCommand, TodoDto>
{
    public async Task<TodoDto> Handle(CreateTodoCommand request, CancellationToken cancellationToken)
    {
        var todo = new Todo
        {
            Title = request.Title,
            Description = request.Description,
            IsCompleted = false,
            CreatedAt = DateTime.UtcNow
        };

        db.Todos.Add(todo);
        await db.SaveChangesAsync(cancellationToken);

        return new TodoDto(todo.Id, todo.Title, todo.Description, todo.IsCompleted, todo.CreatedAt, todo.UpdatedAt);
    }
}
