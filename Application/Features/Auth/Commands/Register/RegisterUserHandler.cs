using Application.Core;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Auth.Commands.Register;

public class RegisterUserHandler(UserManager<AppUserEntity> userManager) 
    : IRequestHandler<RegisterUserCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(RegisterUserCommand request, CancellationToken ct)
    {
        var user = new AppUserEntity
        {
            UserName = request.UserName,
            Email = request.Email,
            Country = request.Country,
        };

        var createUserResult = await userManager.CreateAsync(user, request.Password);

        if (!createUserResult.Succeeded)
            return Result<Unit>.Failure(string.Join(", ", createUserResult.Errors.Select(e => e.Description)),400);

        return Result<Unit>.Success(Unit.Value);

    }
} 