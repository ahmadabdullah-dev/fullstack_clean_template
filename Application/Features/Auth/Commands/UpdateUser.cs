using Application.Features.Auth.DTOs;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Auth.Commands;

public class UpdateUser
{
    public record Command : IRequest<Result<string>>
    {
        public required UpdateUserDto Dto { get; init; }
    }
    public class Handler(AppDbContext context, IUserAccessor userAccessor, IValidator<Command> validator) : IRequestHandler<Command, Result<string>>
    {
        public async Task<Result<string>> Handle(Command request, CancellationToken ct)
        {
            var validationResult = await validator.ValidateAsync(request, ct);

            if (!validationResult.IsValid)
                return Result<string>.Failure(string.Join(",", validationResult.Errors.Select(e => e.ErrorMessage)), 400);

            var user = await userAccessor.GetUserAsync();
        
                user.UserName = request.Dto.UserName;

                user.Email = request.Dto.Email;

                user.Country = request.Dto.Country;
          
                user.PasswordHash = request.Dto.NewPassword;
            
                context.Entry(user).State = EntityState.Modified;

            var result = await context.SaveChangesAsync(ct) > 0;

            return result
                ? Result<string>.Success(user.Id)
                : Result<string>.Failure("Unexpected error happened", 500);
        }
    }
}
