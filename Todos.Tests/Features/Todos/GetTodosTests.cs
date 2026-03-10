using System.Net;
using System.Net.Http.Json;
using Todos.Api.Features.Todos.GetTodos;

namespace Todos.Tests.Features.Todos;

public class GetTodosTests(TodosApiFactory factory) : IClassFixture<TodosApiFactory>
{
    private readonly HttpClient _client = factory.CreateClient();

    [Fact]
    public async Task GetAll_EmptyDatabase_ReturnsEmptyList()
    {
        await using var localFactory = new TodosApiFactory();
        await localFactory.InitializeAsync();
        var client = localFactory.CreateClient();

        var response = await client.GetAsync("/api/todos");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var todos = await response.Content.ReadFromJsonAsync<List<TodoDto>>();
        Assert.NotNull(todos);
        Assert.Empty(todos);
    }

    [Fact]
    public async Task GetAll_AfterCreatingTodos_ReturnsThem()
    {
        await _client.PostAsJsonAsync("/api/todos", Any.CreateTodoRequest("First"));
        await _client.PostAsJsonAsync("/api/todos", Any.CreateTodoRequest("Second"));

        var response = await _client.GetAsync("/api/todos");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var todos = await response.Content.ReadFromJsonAsync<List<TodoDto>>();
        Assert.NotNull(todos);
        Assert.True(todos.Count >= 2);
        Assert.Contains(todos, t => t.Title == "First");
        Assert.Contains(todos, t => t.Title == "Second");
    }

    [Fact]
    public async Task GetById_ExistingTodo_ReturnsTodo()
    {
        var created = await CreateTodo("Find me");

        var response = await _client.GetAsync($"/api/todos/{created.Id}");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var todo = await response.Content.ReadFromJsonAsync<TodoDto>();
        Assert.NotNull(todo);
        Assert.Equal(created.Id, todo.Id);
        Assert.Equal("Find me", todo.Title);
    }

    [Fact]
    public async Task GetById_NonExistingTodo_ReturnsNotFound()
    {
        var response = await _client.GetAsync("/api/todos/99999");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task GetAll_ReturnsMostRecentFirst()
    {
        await using var localFactory = new TodosApiFactory();
        await localFactory.InitializeAsync();
        var client = localFactory.CreateClient();

        await client.PostAsJsonAsync("/api/todos", Any.CreateTodoRequest("Older"));
        await Task.Delay(50);
        await client.PostAsJsonAsync("/api/todos", Any.CreateTodoRequest("Newer"));

        var response = await client.GetAsync("/api/todos");
        var todos = await response.Content.ReadFromJsonAsync<List<TodoDto>>();

        Assert.NotNull(todos);
        Assert.Equal(2, todos.Count);
        Assert.Equal("Newer", todos[0].Title);
        Assert.Equal("Older", todos[1].Title);
    }

    private async Task<TodoDto> CreateTodo(string? title = null)
    {
        var response = await _client.PostAsJsonAsync("/api/todos", Any.CreateTodoRequest(title));
        response.EnsureSuccessStatusCode();
        return (await response.Content.ReadFromJsonAsync<TodoDto>())!;
    }
}
