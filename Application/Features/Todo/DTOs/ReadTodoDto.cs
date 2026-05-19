namespace Application.Features.Todo.DTOs;

public record ReadTodoDto
(
    string Id,
    string AppUserId,
    string? Title,
    bool IsCompleted,
    DateTimeOffset CreatedDate

);
