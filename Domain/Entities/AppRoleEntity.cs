using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public class AppRoleEntity : IdentityRole
{
    public string? Description { get; set; }
}