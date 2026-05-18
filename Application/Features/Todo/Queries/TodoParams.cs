namespace Application.Features.Todo.Queries;

public class TodoParams : PaginationParams
{
    public DateTimeOffset StartDate { get; set; }
}