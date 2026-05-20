using Application.Features.Auth.Commands;
using Application.Features.Auth.DTOs;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
namespace API.Controllers;

public class AuthController(SignInManager<AppUserEntity> signInManager) : BaseApiController
{

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<ActionResult> Register([FromBody] CreateUserDto dto)
    {
        return HandleResult(await Mediator.Send(new CreateUser.Command { Dto = dto }));
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult> Login([FromBody] LoginUserDto dto)
    {
        var result = await Mediator.Send(new LoginUser.Command { Dto = dto });

        if (!result.IsSuccess)
            return Unauthorized(result.Error);

        await signInManager.SignInAsync(result.Value!, isPersistent: dto.RememberMe); // isPersistent: determines if the browser will save login cookies 

        return NoContent();
    }
    [Authorize]
    [HttpPost("logout")]
    public async Task<ActionResult> Logout()
    {
        await signInManager.SignOutAsync();
        return NoContent();
    }
}
