using System.Net;
using System.Net.Http.Json;
using Todos.Api.Features.Todos.GetTodos;

namespace Todos.Tests.Features.Todos;

public class UpdateTodoTests(TodosApiFactory factory) : IClassFixture<TodosApiFactory>
{
    private readonly HttpClient _client = factory.CreateClient();

    [Fact]
    public async Task Update_ExistingTodo_ReturnsUpdatedTodo()
    {
        var created = await CreateTodo();

        var updateRequest = Any.UpdateTodoRequest("Updated Title", "Updated Description");
        var response = await _client.PutAsJsonAsync($"/api/todos/{created.Id}", updateRequest);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var updated = await response.Content.ReadFromJsonAsync<TodoDto>();
        Assert.NotNull(updated);
        Assert.Equal(created.Id, updated.Id);
        Assert.Equal("Updated Title", updated.Title);
        Assert.Equal("Updated Description", updated.Description);
        Assert.False(updated.IsCompleted);
        Assert.NotNull(updated.UpdatedAt);
    }

    [Fact]
    public async Task Update_MarkAsCompleted_SetsIsCompletedTrue()
    {
        var created = await CreateTodo();

        var updateRequest = Any.CompletedTodoRequest(created.Title);
        var response = await _client.PutAsJsonAsync($"/api/todos/{created.Id}", updateRequest);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var updated = await response.Content.ReadFromJsonAsync<TodoDto>();
        Assert.NotNull(updated);
        Assert.True(updated.IsCompleted);
    }

    [Fact]
    public async Task Update_NonExistingTodo_ReturnsNotFound()
    {
        var updateRequest = Any.UpdateTodoRequest();

        var response = await _client.PutAsJsonAsync("/api/todos/99999", updateRequest);

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task Update_PreservesCreatedAt()
    {
        var created = await CreateTodo();

        var updateRequest = Any.UpdateTodoRequest();
        var response = await _client.PutAsJsonAsync($"/api/todos/{created.Id}", updateRequest);

        var updated = await response.Content.ReadFromJsonAsync<TodoDto>();
        Assert.NotNull(updated);
        Assert.Equal(created.CreatedAt, updated.CreatedAt);
    }

    private async Task<TodoDto> CreateTodo()
    {
        var response = await _client.PostAsJsonAsync("/api/todos", Any.CreateTodoRequest());
        response.EnsureSuccessStatusCode();
        return (await response.Content.ReadFromJsonAsync<TodoDto>())!;
    }
}
