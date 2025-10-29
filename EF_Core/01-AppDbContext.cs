using EF_Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EF_Core;

public abstract class Connection
{
    public static string connectionString = String.Empty;

    public static void SetConnectionString()
    {
        var config = new ConfigurationBuilder()
          .AddJsonFile("appsettings.json")
          .Build();

        connectionString = config.GetSection("constr").Value!;

        Console.WriteLine(connectionString);
    }
}

public class AppDbContext01 : DbContext
{
    public DbSet<Wallet> Wallets { get; set; } = null!;

    // Internal Configuration
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder.UseSqlServer(Connection.connectionString);
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