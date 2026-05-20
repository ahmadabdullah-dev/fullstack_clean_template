using Application.Features.Auth.DTOs;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Auth.Commands;

public class LoginUser
{
    public record Command : IRequest<Result<AppUserEntity>>
    {
        public required LoginUserDto Dto { get; init; }
    }

    public class Handler(UserManager<AppUserEntity> _userManager, IValidator<Command> validator) 
        : IRequestHandler<Command, Result<AppUserEntity>>
    {
        public async Task<Result<AppUserEntity>> Handle(Command request, CancellationToken ct)
        {
            var validationResult = await validator.ValidateAsync(request, ct);

            if (!validationResult.IsValid)
                return Result<AppUserEntity>.Failure(string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)), 400);

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