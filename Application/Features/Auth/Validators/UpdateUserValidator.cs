using Application.Features.Auth.Commands;
using FluentValidation;

public class UpdateUserValidator : AbstractValidator<UpdateUser.Command>
{
    public UpdateUserValidator()
    {
        RuleFor(x => x.Dto.UserName)
            .MaximumLength(256)
            .WithName("UserName")
            .When(x => !string.IsNullOrWhiteSpace(x.Dto.UserName));
       
        RuleFor(x => x.Dto.Email)
            .EmailAddress()
            .WithName("Email")
            .When(x => !string.IsNullOrWhiteSpace(x.Dto.Email));

        RuleFor(x => x.Dto.NewPassword)
            .Length(8, 256)
            .WithName("New Password")
            .When(x => !string.IsNullOrWhiteSpace(x.Dto.NewPassword));
      
        RuleFor(x => x.Dto.CurrentPassword)
          .NotEmpty()
          .WithMessage("Current password is required to set a new password")
          .When(x => !string.IsNullOrWhiteSpace(x.Dto.NewPassword));
      
        RuleFor(x => x.Dto.Country)
            .Length(2, 20)
            .WithName("Country")
            .When(x => !string.IsNullOrWhiteSpace(x.Dto.Country));
    }
}