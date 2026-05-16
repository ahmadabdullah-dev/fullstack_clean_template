using API.DTOs;
using Application.Features.Auth.Commands.Register;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class AuthController : BaseApiController
{
    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<ActionResult> RegisterUser([FromBody] RegisterDto dto)
    {
        var command = new RegisterCommand(dto.UserName,dto.Email,dto.Password,dto.Country);
        return HandleResult(await Mediator.Send(command));
    }
}
