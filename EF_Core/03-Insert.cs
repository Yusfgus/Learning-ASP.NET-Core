using Shared;
using EF_Core.Entities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace EF_Core;

public class Insert
{
    public static void Run()
    {
        Utils.printTitle(title: "Insert ( using Context Factory )", color: ConsoleColor.Blue, width: 70);
        
        var walletToInsert = new Wallet();

        Console.Write("Enter wallet holder: ");
        walletToInsert.Holder = Console.ReadLine();

        Console.Write("Enter wallet balance: ");
        walletToInsert.Balance = Convert.ToInt32(Console.ReadLine());

        var services = new ServiceCollection();

        services.AddDbContextFactory<AppDbContext02>(options =>
            options.UseSqlServer(Connection.connectionString)
        );

        IServiceProvider serviceProvider = services.BuildServiceProvider();

        var contextFactory = serviceProvider.GetService<IDbContextFactory<AppDbContext02>>();

        using (var context = contextFactory!.CreateDbContext())
        {
            context.Wallets.Add(walletToInsert);

            context.SaveChanges();

            Console.WriteLine($"Wallet {{ {walletToInsert} }} added successfully");
        }
    }
}