using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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
        else
        {
            users = await userManager.Users.ToListAsync();  // use existing users to add id AppUserId for todos
        }

        if (context.Todos.Any()) 
            return;

        var todos = new List<Todo>()
        {
           new() {
                Title = "Go to university",
                IsCompleted = true,
                CreatedDate = DateTimeOffset.UtcNow.AddDays(-3),
                AppUserId = users[0].Id
            },
            new() {
                Title = "Go to pazzar",
                IsCompleted = false,
                CreatedDate = DateTimeOffset.UtcNow.AddDays(-1),
                AppUserId = users[0].Id
            },
            new() {
                Title = "Learn German",
                IsCompleted = false,
                CreatedDate = DateTimeOffset.UtcNow.AddDays(-5),
                AppUserId = users[1].Id
            },
            new() {
                Title = "Learn Coding",
                IsCompleted = true,
                CreatedDate = DateTimeOffset.UtcNow.AddDays(-7),
                AppUserId = users[1].Id
            },
            new() {
                Title = "Go to gym",
                IsCompleted = false,
                CreatedDate = DateTimeOffset.UtcNow.AddDays(-2),
                AppUserId = users[2].Id
            },
        };

        context.Todos.AddRange(todos);
        await context.SaveChangesAsync();

    }
}
