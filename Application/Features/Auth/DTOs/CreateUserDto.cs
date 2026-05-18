namespace Application.Features.Auth.DTOs;
public record CreateUserDto(
  string UserName,
  string Email,
  string Password,
  string Country
  );
