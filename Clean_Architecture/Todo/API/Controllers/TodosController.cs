using Application.Todos.Commands.CreateTodo;
using Application.Todos.Commands.UpdateTodo;
using Application.Todos.Commands.DeleteTodo;
using Application.Todos.Queries.GetTodos;
using Domain.Todos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using API.Requests;

namespace API.Controllers;

[ApiController]
[Route("api/todos")]
public class TodosController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        List<Todo> result = await mediator.Send(new GetTodosQuery());
        return Ok(result);
    }

    [HttpGet("{id:guid}", Name = nameof(GetById))]
    public async Task<IActionResult> GetById(Guid id)
    {
        Todo? result = await mediator.Send(new GetTodoByIdQuery(id));
        return result is not null ? Ok(result) : NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateTodoRequest request)
    {
        CreateTodoCommand command = new(request.Title);
        Guid id = await mediator.Send(command);
        return CreatedAtRoute(nameof(GetById), new {id}, null);
    } 

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateTodoRequest request)
    {
        UpdateTodoCommand command = new(id, request.Title, request.Completed);
        await mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await mediator.Send(new DeleteTodoCommand(id));
        return NoContent();
    }
}