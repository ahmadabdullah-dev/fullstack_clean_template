using Application.Core;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Domain.Entities;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BaseApiController : ControllerBase
{
    private IMediator? _mediator;
    private UserManager<AppUserEntity>? _userManager;
    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>()
        ?? throw new InvalidOperationException("IMediator service is not available.");
    protected UserManager<AppUserEntity> UserManager => _userManager ??=
    HttpContext.RequestServices.GetService<UserManager<AppUserEntity>>()
    ?? throw new InvalidOperationException("UserManager service is not available.");

    protected ActionResult HandleResult<T>(Result<T> result)
    {
        if (result.IsSuccess && result.Value != null)
            return Ok(result.Value);

        if (!result.IsSuccess && result.Code == 404)
            return NotFound();

        return BadRequest(result.Error);
    }
}