namespace API.DTOs;

public record RegisterDto(
    string UserName,
    string Email, 
    string Password,
    string Country
);

public record LoginDto(
    string Email, 
    string Password
);
