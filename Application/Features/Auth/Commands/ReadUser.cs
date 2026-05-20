using Application.Features.Auth.DTOs;
using MediatR;

namespace Application.Features.Auth.Commands;
public class ReadUser
{

    public class Query : IRequest<Result<ReadUserDto>>
    {
        public required string Id { get; init; }
    }
    public class Handler(AppDbContext context) : IRequestHandler<Query,Result<ReadUserDto>>
    {
        public async Task<Result<ReadUserDto>> Handle(Query request, CancellationToken ct)
        {
            var user = await context.Users.FindAsync([request.Id], ct); // [] seperate key from ct

            if (user == null) 
                return Result<ReadUserDto>.Failure("User not found",404);

            var dto = new ReadUserDto(user.Id, user.UserName!, user.Country!, user.Email!);

            return Result<ReadUserDto>.Success(dto);
        }
    }

}
