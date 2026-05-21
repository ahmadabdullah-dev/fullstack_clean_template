using Application.Features.Admin.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize(Roles = "Admin")]
public class AdminController : BaseApiController
{
    [HttpPatch("assignmembertoadmin/{id}")]
    public async Task<ActionResult> AssignMemberToAdmin(string id)
    {
        return HandleResult(await Mediator.Send(new AssignMemberToAdmin.Command { Id = id }));
    }
}
