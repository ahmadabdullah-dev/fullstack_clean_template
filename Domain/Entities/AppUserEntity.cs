using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public class AppUserEntity : IdentityUser
{
    public string? Country { get; set; }
    public IEnumerable<TodoEntity>? Todos { get; set; }
}
