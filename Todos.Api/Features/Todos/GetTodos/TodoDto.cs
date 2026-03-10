namespace Todos.Api.Features.Todos.GetTodos;

public record TodoDto(int Id, string Title, string? Description, bool IsCompleted, DateTime CreatedAt, DateTime? UpdatedAt);
