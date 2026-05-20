using Application.Features.Todo.DTOs;
using FluentValidation;
using MediatR;

namespace Application.Features.Todo.Commands;

public class UpdateTodo
{
    public record Command : IRequest<Result<string>>
    {
        public required string Id { get; init; }
        public required UpdateTodoDto Dto { get; init; }
    }
    public class Handler(AppDbContext context, IUserAccessor userAccessor, IValidator<Command> validator) : IRequestHandler<Command, Result<string>>
    {
        public async Task<Result<string>> Handle(Command request, CancellationToken ct)
        {
            var validationResult = await validator.ValidateAsync(request,ct);

            if (!validationResult.IsValid)
                return Result<string>.Failure(string.Join(",", validationResult.Errors.Select(e => e.ErrorMessage)), 400);
          
            var user = await userAccessor.GetUserAsync();
            var todo = await context.Todos.FindAsync([request.Id],ct);

            if (todo == null)
                return Result<string>.Failure("Todo was not found", 404);

            if (todo.AppUserId != user.Id.ToString()) 
                return Result<string>.Failure("No ability to update this todo",403);

            if(!string.IsNullOrWhiteSpace(todo.Title))
               todo.Title = request.Dto.Title;
           
            var result = await context.SaveChangesAsync(ct) > 0;
            
            return result 
                ? Result<string>.Success(todo.Id)
                : Result<string>.Failure("Unexpected error happened", 500);
        }   
    }
}
