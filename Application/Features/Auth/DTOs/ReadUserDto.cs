namespace Application.Features.Auth.DTOs;

public record ReadUserDto(
    string Id,
    string UserName,
    string Country,
    string Email
    );
