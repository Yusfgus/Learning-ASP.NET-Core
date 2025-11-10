using System.Linq;
using EF_Core.Transactions.Data;
using EF_Core.Transactions.Entities;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace EF_Core.Transactions.Helpers;

public static class DatabaseHelper
{
    public static void RecreateCleanDatabase()
    {
        using var context = new AppDbContext();

        if (context.Database.EnsureDeleted())
            System.Console.WriteLine("Database deleted successfully");

        if (context.Database.EnsureCreated())
            System.Console.WriteLine("Database created successfully");
    }

    public static void PopulateDatabase()
    {
        using (var context = new AppDbContext())
        {
            context.Add(
                new BankAccount
                {
                    AccountId = "1",
                    AccountHolder = "Ahmed Ali",
                    CurrentBalance = 10000m
                });

            context.Add(
                new BankAccount
                {
                    AccountId = "2",
                    AccountHolder = "Sameh Ahmed",
                    CurrentBalance = 14000
                });

            context.Add(
                new BankAccount
                {
                    AccountId = "3",
                    AccountHolder = "Reem Ali",
                    CurrentBalance = 15000
                });

            context.SaveChanges();

            System.Console.WriteLine("Database populated successfully");
        }
    }

    public static void CleanDatabase()
    {
        using (var context = new AppDbContext())
        {
            var tableNames = context.Model.GetEntityTypes()
                            .Select(t => t.GetTableName())
                            .Distinct()
                            .ToList();

            foreach (var tableName in tableNames)
            {
                System.Console.WriteLine($"Delete table {tableName}");
                context.Database.ExecuteSqlRaw($"DELETE FROM [{tableName}]");
            }

            System.Console.WriteLine("Database cleared successfully");
        }
    }

    public static void InitializeDatabase()
    {
        CleanDatabase();
        PopulateDatabase();
    }

    public static void ShowAccounts()
    {
        using (var context = new AppDbContext())
        {
            context.BankAccounts.Print("Bank Accounts");
        }
    }

}