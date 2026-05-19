using Application.Features.Todo.DTOs;
using MediatR;

namespace Application.Features.Todo.Commands;

public class UpdateTodo
{
    public record Command : IRequest<Result<string>>
    {
        public required string Id { get; init; }
        public required UpdateTodoDto Dto { get; init; }
    }
    public class Handler(AppDbContext context, IUserAccessor userAccessor) : IRequestHandler<Command, Result<string>>
    {
        public async Task<Result<string>> Handle(Command request, CancellationToken ct)
        {
            var user = await userAccessor.GetUserAsync();
            var todo = await context.Todos.FindAsync(request.Id);

            if (todo == null)
                return Result<string>.Failure("Todo was not found", 404);

            if (todo.AppUserId != user.Id.ToString()) 
                return Result<string>.Failure("No ability to update this todo",400);

            if(request.Dto.Title != null && request.Dto.Title != "")
               todo.Title = request.Dto.Title;
           
            var result = await context.SaveChangesAsync(ct) > 0;
            
            return result 
                ? Result<string>.Success(todo.Id)
                : Result<string>.Failure("Unexpected error happened", 400);
        }   
    }
}
