using Application.Features.User.DTOs;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.User.Commands;

public class UpdateUser
{
    public record Command : IRequest<Result<string>>
    {
        public required UpdateUserDto Dto { get; init; }
    }
    public class Handler(UserManager<AppUserEntity> userManager, IUserAccessor userAccessor, IValidator<Command> validator) : IRequestHandler<Command, Result<string>>
    {
        public async Task<Result<string>> Handle(Command request, CancellationToken ct)
        {
            var validationResult = await validator.ValidateAsync(request, ct);

            if (!validationResult.IsValid)
                return Result<string>.Failure(string.Join(",", validationResult.Errors.Select(e => e.ErrorMessage)), 400);

            var user = await userAccessor.GetUserAsync();

            if (!string.IsNullOrWhiteSpace(request.Dto.UserName))
            {
                var UserNameChangeResult = await userManager.SetUserNameAsync(user, request.Dto.UserName);
               
                if (!UserNameChangeResult.Succeeded)
                    return Result<string>.Failure(string.Join(", ", UserNameChangeResult.Errors.Select(e => e.Description)), 400);
            }

            if (!string.IsNullOrWhiteSpace(request.Dto.Email))
            {
                var EmailChangeResult = await userManager.SetEmailAsync(user, request.Dto.Email);
               
                if (!EmailChangeResult.Succeeded)
                    return Result<string>.Failure(string.Join(", ", EmailChangeResult.Errors.Select(e => e.Description)), 400);
            }


            if (!string.IsNullOrEmpty(request.Dto.Country))
                user.Country = request.Dto.Country;

            if (!string.IsNullOrWhiteSpace(request.Dto.NewPassword) && !string.IsNullOrWhiteSpace(request.Dto.CurrentPassword))
            {
                var ChangePasswordResult = await userManager.ChangePasswordAsync(user, request.Dto.CurrentPassword, request.Dto.NewPassword);
              
                if (!ChangePasswordResult.Succeeded)
                    return Result<string>.Failure(string.Join(", ", ChangePasswordResult.Errors.Select(e => e.Description)), 400);
            }

            var updateResult = await userManager.UpdateAsync(user);
           
            return updateResult.Succeeded
                ? Result<string>.Success(user.Id)
                : Result<string>.Failure(string.Join(", ", updateResult.Errors.Select(e => e.Description)), 500);
        }
    }
}
