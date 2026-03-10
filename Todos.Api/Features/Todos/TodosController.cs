using MediatR;
using Microsoft.AspNetCore.Mvc;
using Todos.Api.Features.Todos.CreateTodo;
using Todos.Api.Features.Todos.GetTodoById;
using Todos.Api.Features.Todos.GetTodos;
using Todos.Api.Features.Todos.UpdateTodo;

namespace Todos.Api.Features.Todos;

[ApiController]
[Route("api/[controller]")]
public class TodosController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<TodoDto>>> GetAll(CancellationToken ct) =>
        Ok(await mediator.Send(new GetTodosQuery(), ct));

    [HttpGet("{id:int}")]
    public async Task<ActionResult<TodoDto>> GetById(int id, CancellationToken ct)
    {
        var result = await mediator.Send(new GetTodoByIdQuery(id), ct);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<TodoDto>> Create([FromBody] CreateTodoRequest request, CancellationToken ct)
    {
        var result = await mediator.Send(new CreateTodoCommand(request.Title, request.Description), ct);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<TodoDto>> Update(int id, [FromBody] UpdateTodoRequest request, CancellationToken ct)
    {
        var result = await mediator.Send(new UpdateTodoCommand(id, request.Title, request.Description, request.IsCompleted), ct);
        return result is null ? NotFound() : Ok(result);
    }
}
