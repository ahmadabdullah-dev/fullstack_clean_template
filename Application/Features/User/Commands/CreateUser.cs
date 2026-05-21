using MediatR;
using Microsoft.AspNetCore.Identity;
using FluentValidation;
using Application.Features.User.DTOs;
namespace Application.Features.User.Commands;

public class CreateUser
{
    public record Command() : IRequest<Result<string>>
    {
        public required CreateUserDto Dto { get; init; }
    }

    public class Handler(UserManager<AppUserEntity> userManager, IValidator<Command> validator)
        : IRequestHandler<Command, Result<string>>
    {
        public async Task<Result<string>> Handle(Command request, CancellationToken ct)
        {
            var validationResult = await validator.ValidateAsync(request,ct);

            if (!validationResult.IsValid)
                return Result<string>.Failure(string.Join(",", validationResult.Errors.Select(e => e.ErrorMessage)), 400);

            var user = new AppUserEntity
            {
                UserName = request.Dto.UserName.ToLowerInvariant().Trim(), // prefred Invariant to ToLower  "I" => "ı" bug 
                Email = request.Dto.Email.ToLowerInvariant().Trim(),
                Country = request.Dto.Country.ToLowerInvariant(),
            };

            var result = await userManager.CreateAsync(user, request.Dto.Password);  
            
            await userManager.AddToRoleAsync(user, "Member");

           
            return result.Succeeded
                ? Result<string>.Success(user.Id)
                : Result<string>.Failure(string.Join(",", result.Errors.Select(e => e.Description)), 400 );
        }
    }
}