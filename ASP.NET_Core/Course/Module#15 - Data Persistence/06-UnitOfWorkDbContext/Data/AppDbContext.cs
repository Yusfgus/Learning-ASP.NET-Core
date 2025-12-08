using UnitOfWorkDbContext.Models;
using Microsoft.EntityFrameworkCore;

namespace UnitOfWorkDbContext.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Product> Products {get; set;}
    public DbSet<ProductReview> ProductReviews {get; set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
