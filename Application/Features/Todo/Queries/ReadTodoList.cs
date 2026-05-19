using Application.Features.Todo.DTOs;
using MediatR;

namespace Application.Features.Todo.Queries;

public class ReadTodoList
{
    public record Query : IRequest<Result<PagedList<ReadTodoDto>>>
    {
        public required TodoParams Params { get; init; }
    }

    public class Handler(AppDbContext context, IUserAccessor userAccessor)
        : IRequestHandler<Query, Result<PagedList<ReadTodoDto>>>
    {
        public async Task<Result<PagedList<ReadTodoDto>>> Handle(Query request, CancellationToken ct)
        {
            var currentUserId = userAccessor.GetUserId();

            var query = context.Todos
                .Where(x => x.AppUserId == currentUserId)
                .Where( x => x.CreatedDate >= request.Params.StartDate)
                .OrderBy(x => x.CreatedDate)
                .Select(x => new ReadTodoDto(x.Id,x.AppUserId!, x.Title, x.IsCompleted, x.CreatedDate));

            var result = await PagedList<ReadTodoDto>.CreateAsync(query, request.Params.Page, request.Params.PageSize, ct);

            return Result<PagedList<ReadTodoDto>>.Success(result);
        }
    }
}