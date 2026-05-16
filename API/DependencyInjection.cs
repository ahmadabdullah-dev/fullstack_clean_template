using Domain.Entities;
using Microsoft.AspNetCore.Identity;

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

        services.AddIdentityApiEndpoints<User>(opt =>
        {
            opt.User.RequireUniqueEmail = true;
           // opt.SignIn.RequireConfirmedEmail = true;
        })
        .AddRoles<IdentityRole>()
        .AddEntityFrameworkStores<AppDbContext>();
        

        return services;
    }
}