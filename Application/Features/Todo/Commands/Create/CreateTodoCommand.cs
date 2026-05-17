using Application.Core;
using MediatR;

namespace Application.Features.Todo.Commands.Create;

public record CreateTodoCommand(
    string AppUserId,
    string Title
    ) : IRequest<Result<Unit>>;