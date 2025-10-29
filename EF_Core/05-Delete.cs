using Shared;
using EF_Core.Entities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace EF_Core;

public class Delete
{
    public static void Run()
    {
        Utils.printTitle(title: "Delete ( using Context Pooling )", color: ConsoleColor.Blue, width: 70);

        Console.Write("Enter wallet id: ");
        int id = Convert.ToInt32(Console.ReadLine());

        var services = new ServiceCollection();

        // max of 1024 instance
        services.AddDbContextPool<AppDbContext02>(options =>
            options.UseSqlServer(Connection.connectionString)
        );

        IServiceProvider serviceProvider = services.BuildServiceProvider();

        using (var context = serviceProvider.GetService<AppDbContext02>()!)
        {
            Wallet? wallet = context.Wallets.SingleOrDefault(x => x.Id == id);

            // context.Remove(wallet!); // also worked
            context.Wallets.Remove(wallet!);

            context.SaveChanges();
            
            Console.WriteLine($"Wallet {{ {wallet} }} deleted successfully");
        }
    }
}