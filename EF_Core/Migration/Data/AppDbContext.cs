using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EF_Core.Migration.Data;

public class AppDbContext: DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        var config = new ConfigurationBuilder()
          .AddJsonFile("appsettings.json")
          .Build();

        string connectionString = config.GetSection("constr").Value!;
        Console.WriteLine(connectionString + '\n');

        optionsBuilder.UseSqlServer(connectionString);
    }
}