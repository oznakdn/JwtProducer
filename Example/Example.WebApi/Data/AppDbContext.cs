using Example.WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace Example.WebApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Role>()
            .HasData(new Role
            {
                Id = 1,
                RoleName ="Admin"
            });

        modelBuilder.Entity<User>()
            .HasData(new User
            {
                Id = 1,
                UserName = "John_Doe",
                Email = "john.doe@mail.com",
                Password = "password123",
                RoleId = 1
            });
    
    }

}
