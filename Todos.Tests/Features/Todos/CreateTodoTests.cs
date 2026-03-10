using System.Net;
using System.Net.Http.Json;
using Todos.Api.Features.Todos.GetTodos;

namespace Todos.Tests.Features.Todos;

public class CreateTodoTests(TodosApiFactory factory) : IClassFixture<TodosApiFactory>
{
    private readonly HttpClient _client = factory.CreateClient();

    [Fact]
    public async Task Create_ReturnsCreatedTodoWithId()
    {
        var request = Any.CreateTodoRequest("Buy milk", "From the supermarket");

        var response = await _client.PostAsJsonAsync("/api/todos", request);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        var todo = await response.Content.ReadFromJsonAsync<TodoDto>();
        Assert.NotNull(todo);
        Assert.True(todo.Id > 0);
        Assert.Equal("Buy milk", todo.Title);
        Assert.Equal("From the supermarket", todo.Description);
        Assert.False(todo.IsCompleted);
    }

    [Fact]
    public async Task Create_WithoutDescription_ReturnsCreatedTodo()
    {
        var request = Any.CreateTodoRequestWithoutDescription("Walk the dog");

        var response = await _client.PostAsJsonAsync("/api/todos", request);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        var todo = await response.Content.ReadFromJsonAsync<TodoDto>();
        Assert.NotNull(todo);
        Assert.Equal("Walk the dog", todo.Title);
        Assert.Null(todo.Description);
    }

    [Fact]
    public async Task Create_LocationHeaderPointsToNewTodo()
    {
        var request = Any.CreateTodoRequest();

        var response = await _client.PostAsJsonAsync("/api/todos", request);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.NotNull(response.Headers.Location);

        var getResponse = await _client.GetAsync(response.Headers.Location);
        Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);
    }
}
