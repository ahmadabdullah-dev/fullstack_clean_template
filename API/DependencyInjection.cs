using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;

namespace API;

public static class DependencyInjection
{
    public static IServiceCollection AddAPI(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("AllowFrontend", policy =>
            {
                policy
                    .WithOrigins("http://localhost:3000")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
            });
        });

        services.AddIdentityApiEndpoints<AppUserEntity>(opt =>
        {
            opt.User.RequireUniqueEmail = true;

            // opt.SignIn.RequireConfirmedEmail = true;

            opt.Lockout.MaxFailedAccessAttempts = 5;
            opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
            opt.Lockout.AllowedForNewUsers = true;
        })
        .AddRoles<AppRoleEntity>()
        .AddEntityFrameworkStores<AppDbContext>();

        services.AddRateLimiter(options =>
        {
            options.AddFixedWindowLimiter("AuthLimiter", opt =>
            {
                opt.PermitLimit = 5;
                opt.Window = TimeSpan.FromMinutes(15);
                opt.QueueLimit = 0; // don't queue requests, reject immediately when limit is hit
            });
            options.OnRejected = async (context, cancellationToken) =>
            {  
                context.HttpContext.Response.StatusCode = 429;
                context.HttpContext.Response.Headers["Retry-After"] = "900";

                await context.HttpContext.Response.WriteAsync("Too many attempts. Try again in 15 minutes.", cancellationToken);

            };
           

        });

        return services;
    }
    public static async Task MigrateAndSeedAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;
        try
        {
            var context = services.GetRequiredService<AppDbContext>();
            var userManager = services.GetRequiredService<UserManager<AppUserEntity>>();
            var roleManager = services.GetRequiredService<RoleManager<AppRoleEntity>>();
            await context.Database.MigrateAsync();
            await DbInitializer.SeedData(context, userManager,roleManager);
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occurred during migration");
        }
    }

    
}