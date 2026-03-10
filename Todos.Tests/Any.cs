using Todos.Api.Features.Todos.CreateTodo;
using Todos.Api.Features.Todos.UpdateTodo;

namespace Todos.Tests;

public static class Any
{
    private static int _counter;

    public static CreateTodoRequest CreateTodoRequest(string? title = null, string? description = null) =>
        new(title ?? $"Todo #{++_counter}", description ?? $"Description for todo #{_counter}");

    public static CreateTodoRequest CreateTodoRequestWithoutDescription(string? title = null) =>
        new(title ?? $"Todo #{++_counter}", null);

    public static UpdateTodoRequest UpdateTodoRequest(string? title = null, string? description = null, bool isCompleted = false) =>
        new(title ?? $"Updated Todo #{++_counter}", description ?? $"Updated description #{_counter}", isCompleted);

    public static UpdateTodoRequest CompletedTodoRequest(string? title = null) =>
        new(title ?? $"Completed Todo #{++_counter}", null, IsCompleted: true);
}
