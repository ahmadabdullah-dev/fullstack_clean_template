using Application.Features.Auth.Commands;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

public class UpdateUserValidator : AbstractValidator<UpdateUser.Command>
{
    public UpdateUserValidator(AppDbContext context)
    {
        RuleFor(x => x.Dto.UserName)
            .MaximumLength(256)
            .WithName("UserName")
            .MustAsync(async (username, ct) =>
                !await context.Users.AnyAsync(u => u.UserName == username, ct))
            .WithMessage("Username is already taken")
            .When(x => !string.IsNullOrWhiteSpace(x.Dto.UserName));

        RuleFor(x => x.Dto.Email)
            .EmailAddress()
            .WithName("Email")
            .MustAsync(async (email, ct) =>
                !await context.Users.AnyAsync(u => u.Email == email, ct))
            .WithMessage("Email is already taken")
            .When(x => !string.IsNullOrWhiteSpace(x.Dto.Email));

        RuleFor(x => x.Dto.NewPassword)
            .Length(8, 256)
            .WithName("New Password")
            .When(x => !string.IsNullOrWhiteSpace(x.Dto.NewPassword));

        RuleFor(x => x.Dto.Country)
            .Length(2, 20)
            .WithName("Country")
            .When(x => !string.IsNullOrWhiteSpace(x.Dto.Country));
    }
}