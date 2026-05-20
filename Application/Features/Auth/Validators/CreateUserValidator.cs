using Application.Features.Auth.Commands;
using FluentValidation;
namespace Application.Features.Auth.Validators;
public class CreateUserValidator : AbstractValidator<CreateUser.Command>
{
    public CreateUserValidator()
    {
        RuleFor(x => x.Dto.UserName).NotEmpty().MaximumLength(256).WithName("UserName");
        RuleFor(x => x.Dto.Email).NotEmpty().EmailAddress().WithName("Email");
        RuleFor(x => x.Dto.Password).NotEmpty().Length(8,256).WithName("Password");
        RuleFor(x => x.Dto.Country).NotEmpty().Length(2, 20).WithName("Country");
    }
}  