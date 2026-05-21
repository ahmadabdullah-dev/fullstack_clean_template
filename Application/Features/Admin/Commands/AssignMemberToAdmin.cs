using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Admin.Commands;

public class AssignMemberToAdmin
{
    public record Command() : IRequest<Result<string>>
    {
        public required string Id { get; init; }
    }

    public class Handler(UserManager<AppUserEntity> userManager)
        : IRequestHandler<Command, Result<string>>
    {
        public async Task<Result<string>> Handle(Command request, CancellationToken ct)
        {
            var user = await userManager.FindByIdAsync(request.Id);
           
            if (user == null)
                return Result<string>.Failure("user not found", 404);

            await userManager.RemoveFromRoleAsync(user, "Member");

            var result = await userManager.AddToRoleAsync(user, "Admin");

            return result.Succeeded
                ? Result<string>.Success(user.Id)
                : Result<string>.Failure(string.Join(",", result.Errors.Select(e => e.Description)), 400);
        }
    }
}
