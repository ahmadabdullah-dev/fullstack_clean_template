using Application.Features.Auth.Commands;
using Application.Features.Auth.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class UserController : BaseApiController
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
}
