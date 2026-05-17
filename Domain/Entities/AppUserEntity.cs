using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public class AppUserEntity : IdentityUser
{
    public string? Country { get; set; }
    IEnumerable<TodoEntity>? Todos { get; set; }
}
