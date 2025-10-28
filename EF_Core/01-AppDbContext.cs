using EF_Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EF_Core.Data;

public class AppDbContext : DbContext
{
    public DbSet<Wallet> Wallets { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var configuration = new ConfigurationBuilder()
           .AddJsonFile("appsettings.json")
           .Build();

        var constr = configuration.GetSection("constr").Value;

        optionsBuilder.UseSqlServer(constr);
    }
}