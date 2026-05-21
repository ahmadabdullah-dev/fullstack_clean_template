using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.User.Commands;

public class DeleteUser
{
    public record Command : IRequest<Result<string>>; // Empty command required for MediatR contract
    public class Handler(UserManager<AppUserEntity> userManager, IUserAccessor userAccessor) : IRequestHandler<Command, Result<string>>
    {
        public async Task<Result<string>> Handle(Command request, CancellationToken ct)
        {
            var user = await userAccessor.GetUserAsync();
           
            if (user == null)                                                                  
                return Result<string>.Failure("User not found", 404);

            var result = await userManager.DeleteAsync(user);

            return result.Succeeded
            ? Result<string>.Success("User Deleted succesfully")
            : Result<string>.Failure(string.Join(",",result.Errors.Select(e => e.Description)), 500);
        }
    }

}