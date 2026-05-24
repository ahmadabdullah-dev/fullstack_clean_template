using Application.Features.User.DTOs;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.User.Queries;

public class ReadCurrentUser
{
    public class Query : IRequest<Result<ReadUserDto>>;
    public class Handler(UserManager<AppUserEntity> userManager, IUserAccessor userAccessor) : IRequestHandler<Query, Result<ReadUserDto>>
    {
        public async Task<Result<ReadUserDto>> Handle(Query request, CancellationToken ct)
        {
            var user = await userAccessor.GetUserAsync();

            if (user == null)
                return Result<ReadUserDto>.Failure("User not found", 404);

            var role = await userManager.GetRolesAsync(user);

            var dto = new ReadUserDto(user.Id, user.UserName!,user.Email!, user.Country!, role[0]);

            return Result<ReadUserDto>.Success(dto);
        }
    }

}
