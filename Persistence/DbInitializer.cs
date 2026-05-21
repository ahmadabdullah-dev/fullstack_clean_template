using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public class DbInitializer
{
    public static async Task SeedData(AppDbContext context,UserManager<AppUserEntity> userManager, RoleManager<AppRoleEntity> roleManager)
    {
        var roles = new List<AppRoleEntity>()
        {
            new() {Name = "Admin", Description =" Admin role"},
            new() {Name = "Member", Description = "standard user"}
        };
      
        foreach (var role in roles) { 
           
            if(!await roleManager.RoleExistsAsync(role.Name!))
            {
                await roleManager.CreateAsync(role);
            }
        
        }
        var users = new List<AppUserEntity>()
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
            await userManager.AddToRoleAsync(users[0], "Admin");
            await userManager.AddToRoleAsync(users[1], "Member");
            await userManager.AddToRoleAsync(users[2], "Member");

        }
        else
        {
            users = await userManager.Users.ToListAsync();  // use existing users to add id AppUserId for todos
        }
   
        if (context.Todos.Any()) 
            return;

        var todos = new List<TodoEntity>()
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
