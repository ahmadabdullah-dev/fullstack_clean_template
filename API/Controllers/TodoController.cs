using Application.Features.Todo.Commands.Create;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class TodoController : BaseApiController
{
    [Authorize]
    [HttpPost]
    public async Task<ActionResult> CreateTodo(string title)
    {
        var command = new CreateTodoCommand(
            AppUserId: UserManager.GetUserId(User)!,
            Title: title
        );

        return HandleResult(await Mediator.Send(command));
    }
}
