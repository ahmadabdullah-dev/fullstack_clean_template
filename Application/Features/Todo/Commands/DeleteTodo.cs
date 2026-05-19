using MediatR;

namespace Application.Features.Todo.Commands;
public class DeleteTodo
{
    public record Command : IRequest<Result<string>>
    {
        public required string TodoId { get; init; }
    }
    public class  Handler(AppDbContext context, IUserAccessor userAccessor) : IRequestHandler<Command,Result<string>>
    {
        public async Task<Result<string>> Handle(Command request, CancellationToken ct)
        {
            var user = await userAccessor.GetUserAsync();
            var todo = await context.Todos.FindAsync(request.TodoId);

            if (todo == null)
                return Result<string>.Failure("Todo was not found", 404);

            if (todo.AppUserId != user.Id.ToString())
                return Result<string>.Failure("No ability to delete this todo", 403);
           
            context.Todos.Remove(todo);
           
            var result = await context.SaveChangesAsync(ct) > 0;

            return result
            ? Result<string>.Success("Todo Deleted succesfully")
            : Result<string>.Failure("Unexpected error happened", 500);
        }
    }

}
