namespace Application.Features.Todo.Commands;
using Application.Features.Todo.DTOs;
using MediatR;
public class CreateTodo
{
    public record Command : IRequest<Result<string>>
    {
        public required CreateTodoDto Dto { get; set; }
    }

    public class Handler(AppDbContext context, IUserAccessor userAccessor) : IRequestHandler<Command, Result<string>>
    {
        public async Task<Result<string>> Handle(Command request, CancellationToken ct)
        {
            var user = await userAccessor.GetUserAsync();

            var todo = new TodoEntity
            {
                Title = request.Dto.Title,
                AppUserId = user.Id,
                IsCompleted = false,
                CreatedDate = DateTimeOffset.UtcNow,
            };
        
            context.Todos.Add(todo);

            var success = await context.SaveChangesAsync(ct) > 0;

            return success
                ? Result<string>.Success(todo.Id)
                : Result<string>.Failure("Unexpected error happeded", 500);
                
        }
    }
}