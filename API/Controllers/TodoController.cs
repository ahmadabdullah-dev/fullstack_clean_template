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
    [HttpGet]
    public async Task<IActionResult> GetTodoList([FromQuery] TodoParams p)
    {
        return HandleResult(await Mediator.Send(new ReadTodoList.Query { Params = p }));
    }
}
