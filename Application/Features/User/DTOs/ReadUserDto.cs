namespace Application.Features.User.DTOs;

public record ReadUserDto(
    string Id,
    string UserName,  
    string Email,
    string Country,  
    string Role
    );
