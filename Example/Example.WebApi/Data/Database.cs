using Example.WebApi.Models;

namespace Example.WebApi.Data;

public class Database
{
    public static void AddUserData(WebApplication app)
    {
        var scope = app.Services.CreateScope();
        var db = scope.ServiceProvider.GetService<AppDbContext>();

        db.Roles.Add(new Role
        {
            Id = 1,
            RoleName = "Admin"
        });

        db.Users.Add(new User
        {
            Id = 1,
            UserName = "John_Doe",
            Email = "john.doe@mail.com",
            Password = "password123",
            RoleId = 1
        });
        

        db.SaveChanges();
    }
}
