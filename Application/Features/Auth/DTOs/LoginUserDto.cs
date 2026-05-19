namespace Application.Features.Auth.DTOs;

public record LoginUserDto(
    string Email,
    string Password,
    bool RememberMe
);
