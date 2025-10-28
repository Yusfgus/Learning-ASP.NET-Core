using EF_Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EF_Core;

public class AppDbContext01 : DbContext
{
    public DbSet<Wallet> Wallets { get; set; } = null!;

    // Internal Configuration
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        var configuration = new ConfigurationBuilder()
           .AddJsonFile("appsettings.json")
           .Build();

        var constr = configuration.GetSection("constr").Value;

        optionsBuilder.UseSqlServer(constr);
    }
}

public class AppDbContext02 : DbContext
{
    public DbSet<Wallet> Wallets { get; set; } = null!;

    // External Configuration
    public AppDbContext02(DbContextOptions options): base(options)
    {
        
    }
}