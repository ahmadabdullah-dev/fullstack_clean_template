using API.DTOs;
using Application.Features.Auth.Commands.Register;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Domain.Entities;
namespace API.Controllers;

public class AuthController : BaseApiController
{
    private readonly SignInManager<AppUser> _signInManager;

    public AuthController(SignInManager<AppUser> signInManager)
    {
        _signInManager = signInManager;
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<ActionResult> RegisterUser([FromBody] RegisterDto dto)
    {
        var command = new RegisterCommand(dto.UserName,dto.Email,dto.Password,dto.Country);
        return HandleResult(await Mediator.Send(command));
    }

    [Authorize]
    [HttpPost("logout")]
    public async Task<ActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return NoContent();
    }
}
