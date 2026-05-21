namespace Application.Features.User.DTOs;
public record CreateUserDto(
  string UserName,
  string Email,
  string Password,
  string Country
  );
