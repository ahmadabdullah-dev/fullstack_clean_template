using Application.Core;
using MediatR;

namespace Application.Features.Auth.Commands.Register;

public record RegisterCommand(
    string UserName,
    string Email,
    string Password,
    string Country
    ) : IRequest<Result<Unit>>;
