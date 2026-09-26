using Microsoft.EntityFrameworkCore;

namespace PizzaZiegWebApp.Domain;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder builder) =>
        base.OnModelCreating(builder);
}
