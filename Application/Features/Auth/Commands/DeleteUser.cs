using MediatR;

namespace Application.Features.Auth.Commands;

public class DeleteUser
{
    public record Command : IRequest<Result<string>>; // Empty command required for MediatR contract
    public class Handler(AppDbContext context, IUserAccessor userAccessor) : IRequestHandler<Command, Result<string>>
    {
        public async Task<Result<string>> Handle(Command request, CancellationToken ct)
        {
            var user = await userAccessor.GetUserAsync();

            context.Users.Remove(user);

            var result = await context.SaveChangesAsync(ct) > 0;

            return result
            ? Result<string>.Success("User Deleted succesfully")
            : Result<string>.Failure("Unexpected error happened", 500);
        }
    }

}