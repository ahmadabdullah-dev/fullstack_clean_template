using Application.Features.Todo.Commands;
using FluentValidation;

namespace Application.Features.Todo.Validators;

public class CreateTodoValidator : AbstractValidator<CreateTodo.Command>
{
    public CreateTodoValidator(){
            RuleFor(x => x.Dto.Title)
                .NotEmpty()
                .MaximumLength(256)
                .WithName("Title");
        }
}

