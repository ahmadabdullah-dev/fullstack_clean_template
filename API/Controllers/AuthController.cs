using API.DTOs;
using Application.Features.Auth.Commands;
using Application.Features.Auth.DTOs;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
namespace API.Controllers;

public class AuthController : BaseApiController
{
    private readonly SignInManager<AppUserEntity> _signInManager;
    private readonly UserManager<AppUserEntity> _userManager;

    public AuthController(SignInManager<AppUserEntity> signInManager, UserManager<AppUserEntity> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<ActionResult> Register([FromBody] CreateUserDto dto)
    {
        return HandleResult(await Mediator.Send(new CreateUser.Command { Dto = dto }));
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult> Login([FromBody] LoginDto dto)
    {
        var user = await _userManager.FindByEmailAsync(dto.Email);
        if (user == null) return Unauthorized("Invalid email or password");

        var result = await _userManager.CheckPasswordAsync(user, dto.Password);
        if (!result) return Unauthorized("Invalid email or password");

        await _signInManager.SignInAsync(user, isPersistent: true);
        return NoContent();
    }

    [Authorize]
    [HttpPost("logout")]
    public async Task<ActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return NoContent();
    }
}
