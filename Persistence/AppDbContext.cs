using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public class AppDbContext(DbContextOptions options) 
    : IdentityDbContext<AppUserEntity, AppRoleEntity, string> (options)
{
    public DbSet<TodoEntity> Todos { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<AppUserEntity>()
       .HasMany(u => u.Todos)
       .WithOne()
       .HasForeignKey(t => t.AppUserId)
       .OnDelete(DeleteBehavior.Cascade);
    }
}
