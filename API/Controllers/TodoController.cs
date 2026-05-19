using Application.Features.Todo.Commands;
using Application.Features.Todo.DTOs;
using Application.Features.Todo.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class TodoController : BaseApiController
{
    [Authorize]
    [HttpPost]
    public async Task<ActionResult> CreateTodo([FromBody] CreateTodoDto dto)
    {
        return HandleResult(await Mediator.Send(new CreateTodo.Command { Dto = dto}));
    }
    [Authorize]
    [HttpGet]
    public async Task<IActionResult> ReadTodoList([FromQuery] TodoParams p)
    {
        return HandleResult(await Mediator.Send(new ReadTodoList.Query { Params = p }));
    }
    [Authorize]
    [HttpPatch("{id}")]
    public async Task<ActionResult> UpdateTodo(string id,[FromBody] UpdateTodoDto dto)
    {
        return HandleResult(await Mediator.Send(new UpdateTodo.Command {Id = id,  Dto = dto }));
    }
    [Authorize]
    [HttpPatch("{id}/complete")]
    public async Task<ActionResult> CompletTodo(string id,[FromBody] bool complete)
    {
        return HandleResult(await (Mediator.Send(new CompleteTodo.Command { Id = id, Complete = complete})));
    }
}
