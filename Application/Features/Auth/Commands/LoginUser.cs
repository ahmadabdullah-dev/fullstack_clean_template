using Application.Features.Auth.DTOs;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Auth.Commands;

public class LoginUser
{
    public record Command : IRequest<Result<AppUserEntity>>
    {
        public required LoginUserDto Dto { get; set; }
    }

    public class Handler : IRequestHandler<Command, Result<AppUserEntity>>
    {
        private readonly UserManager<AppUserEntity> _userManager;

        public Handler(UserManager<AppUserEntity> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Result<AppUserEntity>> Handle(Command request, CancellationToken ct)
        {
            var user = await _userManager.FindByEmailAsync(request.Dto.Email.ToLowerInvariant().Trim());

            if (user == null)
                return Result<AppUserEntity>.Failure("Invalid email or password",400);

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, request.Dto.Password);

            if (!isPasswordValid)
            {
                await _userManager.AccessFailedAsync(user);
                return Result<AppUserEntity>.Failure("Invalid email or password", 400);
            }


            return Result<AppUserEntity>.Success(user);
        }
    }
}