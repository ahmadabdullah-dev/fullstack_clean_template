namespace Application.Features.Todo.Queries;

using Application.Features.Todo.DTOs;
using MediatR;


public class ReadTodo
{
    public record Query : IRequest<Result<ReadTodoDto>>
    {
        public required string Id { get; init; }
    }

    public class Handler(AppDbContext context, IUserAccessor userAccessor)
        : IRequestHandler<Query, Result<ReadTodoDto>>
    {
        public async Task<Result<ReadTodoDto>> Handle(Query request, CancellationToken ct)
        {
            var currentUserId = userAccessor.GetUserId();
            var todo = await context.Todos.FindAsync([request.Id], ct);

            if (todo == null)
                return Result<ReadTodoDto>.Failure("Todo was not found", 404);

            if (todo.AppUserId != currentUserId.ToString())
                return Result<ReadTodoDto>.Failure("No ability to read this todo", 403);
          
            var dto = new ReadTodoDto(todo.Id,todo.AppUserId, todo.Title,todo.IsCompleted,todo.CreatedDate);
            
            return Result<ReadTodoDto>.Success(dto);
        }
    }
}