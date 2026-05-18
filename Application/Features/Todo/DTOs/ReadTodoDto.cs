namespace Application.Features.Todo.DTOs;

public record ReadTodoDto
(
    string Id,
    string? Title,
    bool IsCompleted,
    DateTimeOffset CreatedDate
);
