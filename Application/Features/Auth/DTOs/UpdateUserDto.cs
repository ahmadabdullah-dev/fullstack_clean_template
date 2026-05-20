namespace Application.Features.Auth.DTOs;
public record UpdateUserDto(
  string? UserName,
  string? Email,
  string? CurrentPassword,
  string? NewPassword,
  string? Country
  );

