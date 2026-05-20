namespace Application.Features.User.DTOs;
public record UpdateUserDto(
  string? UserName,
  string? Email,
  string? CurrentPassword,
  string? NewPassword,
  string? Country
  );

