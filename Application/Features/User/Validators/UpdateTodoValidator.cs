using Application.Features.Todo.Commands;
using FluentValidation;

namespace Application.Features.User.Validators;

public class UpdateTodoValidator : AbstractValidator<UpdateTodo.Command>
{
    public UpdateTodoValidator()
    {
        RuleFor(x => x.Dto.Title)
            .NotEmpty()
            .MaximumLength(256)
            .WithName("Title");
    }
}

