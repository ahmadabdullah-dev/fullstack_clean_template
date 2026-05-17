using Microsoft.AspNetCore.Identity;

namespace Persistence;

public class DbInitializer
{
    public static async Task SeedData(AppDbContext context,UserManager<AppUser> userManager)
    {
        var users = new List<AppUser>()
        {
            new() {UserName = "cr7", Email= "cristiano@ronaldo.com", Country ="Portugal"},
            new() {UserName = "lm10", Email = "lionel@messi.com", Country ="Argentina"},
            new() {UserName = "sr4", Email = "sergio@ramos.com", Country="Spain"}
        };

        if(!userManager.Users.Any())
        {
            foreach (var user in users)
            {
                await userManager.CreateAsync(user, "Pa$$w0rd");
            }
        }
    }
}
