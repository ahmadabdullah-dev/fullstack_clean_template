using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public class AppUser : IdentityUser
{
    public string? Country { get; set; }
    IEnumerable<Todo>? Todos { get; set; }
}
