using Application.Features.Auth.Commands;
using FluentValidation;

namespace Application.Features.Auth.Validators;

public class LoginUserValidator : AbstractValidator<LoginUser.Command>
{
    public LoginUserValidator()
    {
        RuleFor(x => x.Dto.Email)
            .NotEmpty()
            .EmailAddress()
            .WithName("Email");

        RuleFor(x => x.Dto.Password)
            .NotEmpty()
            .WithName("Password");
    }
}
