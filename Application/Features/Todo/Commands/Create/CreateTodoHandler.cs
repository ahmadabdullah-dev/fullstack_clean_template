namespace Application.Features.Todo.Commands.Create;
using Application.Core;
using MediatR;

public class RegisterUserHandler(AppDbContext context)
    : IRequestHandler<CreateTodoCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(CreateTodoCommand request, CancellationToken ct)
    {
        var todo = new TodoEntity
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            IsCompleted = false,
            CreatedDate = DateTimeOffset.UtcNow,
            AppUserId = request.AppUserId,
        };

        context.Todos.Add(todo);
        await context.SaveChangesAsync(ct);

        return Result<Unit>.Success(Unit.Value);

    }
}