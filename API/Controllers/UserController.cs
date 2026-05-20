using Application.Features.Auth.Commands;
using Application.Features.Auth.DTOs;
using Application.Features.Auth.Queries;
using Domain.Entities;
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
