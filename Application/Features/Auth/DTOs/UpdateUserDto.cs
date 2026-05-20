namespace Application.Features.Auth.DTOs;
public record UpdateUserDto(
  string UserName,
  string Email,
  string NewPassword,
  string Country
  );

