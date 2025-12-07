using UnitOfWork.Models;
using Microsoft.EntityFrameworkCore;

namespace UnitOfWork.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Product> Products {get; set;}
    public DbSet<ProductReview> ProductReviews {get; set;}

    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    // {
    //     base.OnConfiguring(optionsBuilder);

    //     optionsBuilder.UseSqlite("Data Source = app.db");
    // }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
