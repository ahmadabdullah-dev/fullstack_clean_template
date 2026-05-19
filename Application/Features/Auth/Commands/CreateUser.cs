using Application.Features.Auth.DTOs;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Auth.Commands;

public class CreateUser
{
    public record Command() : IRequest<Result<string>>
    {
        public required CreateUserDto Dto { get; set; }
    }

    public class CreateUserHandler(UserManager<AppUserEntity> userManager) : IRequestHandler<Command, Result<string>>
    {
        public async Task<Result<string>> Handle(Command request, CancellationToken ct)
        {
            var user = new AppUserEntity
            {
                UserName = request.Dto.UserName.ToLowerInvariant().Trim(), // prefred Invariant to ToLower  "I" => "ı" bug 
                Email = request.Dto.Email.ToLowerInvariant().Trim(),
                Country = request.Dto.Country.ToLowerInvariant(),
            };

            var result = await userManager.CreateAsync(user, request.Dto.Password);

            return result.Succeeded
                ? Result<string>.Success(user.Id)
                : Result<string>.Failure(string.Join(",", result.Errors.Select(e => e.Description)), 400 );
        }
    }
}