namespace Todos.Api.Features.Todos.UpdateTodo;

public record UpdateTodoRequest(string Title, string? Description, bool IsCompleted);
