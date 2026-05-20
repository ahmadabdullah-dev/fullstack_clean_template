namespace Application.Features.User.DTOs;

public record ReadUserDto(
    string Id,
    string UserName,
    string Country,
    string Email
    );
