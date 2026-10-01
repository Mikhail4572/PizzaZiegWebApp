using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace PizzaZiegWebApp.Domain;

public class AppDbContext : IdentityDbContext<IdentityUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        string adminName = "admin";
        string roleAdminId = "B50D7436-CEF1-4347-9C78-ABBCD6BCFCA9";
        string userAdminId = "F741C0E5-A211-47E0-B8C6-01810B2A757D";

        //добавляем роль админа
        builder.Entity<IdentityRole>().HasData(new IdentityRole
        {
            Id = roleAdminId,
            Name = adminName,
            NormalizedName = adminName.ToUpper()
        });

        //добавляем нового пользователя в качестве админа
        builder.Entity<IdentityUser>().HasData(new IdentityUser
        {
            Id = userAdminId,
            UserName = adminName,
            NormalizedUserName = adminName.ToUpper(),
            Email = "admin@admin.com",
            NormalizedEmail = "admin@admin.com".ToUpper(),
            EmailConfirmed = true,
            PasswordHash = new PasswordHasher<IdentityUser>()
                           .HashPassword(new IdentityUser(), adminName),
            SecurityStamp = string.Empty,
            PhoneNumberConfirmed = true
        });

        //привязываем роль к пользователю
        builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
        {
            RoleId = roleAdminId,
            UserId = userAdminId
        });
    }
}
