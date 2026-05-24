using Application.Features.User.Commands;
using Application.Features.User.DTOs;
using Application.Features.User.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class UserController(SignInManager<AppUserEntity> signInManager) : BaseApiController
{
    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<IActionResult> ReadUser(string id)
    {
        return HandleResult(await Mediator.Send(new ReadUser.Query { Id = id}));
    }
    [HttpGet]
    public async Task<IActionResult> ReadCurrentUser()
    {
        return HandleResult(await Mediator.Send(new ReadCurrentUser.Query { }));
    }
    [HttpPut]
    public async Task<IActionResult> UpdateUser(UpdateUserDto dto)
    {
        return HandleResult(await Mediator.Send(new UpdateUser.Command { Dto = dto }));
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteUser(CancellationToken ct)
    {
        var result = await Mediator.Send(new DeleteUser.Command());

        if (result.IsSuccess)
            await signInManager.SignOutAsync();

        return HandleResult(result);
    }
}
