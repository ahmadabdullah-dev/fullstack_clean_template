using Application.Features.Todo.Commands;
using Application.Features.Todo.DTOs;
using Application.Features.Todo.Queries;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class TodoController : BaseApiController
{
    [HttpPost]
    public async Task<ActionResult> CreateTodo([FromBody] CreateTodoDto dto)
    {
        return HandleResult(await Mediator.Send(new CreateTodo.Command { Dto = dto}));
    }

    [HttpGet]
    public async Task<IActionResult> ReadTodoList([FromQuery] TodoParams p)
    {
        return HandleResult(await Mediator.Send(new ReadTodoList.Query { Params = p }));
    }

    [HttpGet("todo/{id}")]
    public async Task<IActionResult> ReadTodo(string id)
    {
        return HandleResult(await Mediator.Send(new ReadTodo.Query { Id = id }));
    }

    [HttpPatch("{id}")]
    public async Task<ActionResult> UpdateTodo(string id,[FromBody] UpdateTodoDto dto)
    {
        return HandleResult(await Mediator.Send(new UpdateTodo.Command {Id = id,  Dto = dto }));
    }

    [HttpPatch("{id}/complete")]
    public async Task<ActionResult> CompletTodo(string id)
    {
        return HandleResult(await (Mediator.Send(new CompleteTodo.Command { Id = id})));
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteTodo(string id)
    {
        return HandleResult(await (Mediator.Send(new DeleteTodo.Command { TodoId = id})));
    }
}
