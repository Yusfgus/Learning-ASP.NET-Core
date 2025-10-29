using Shared;
using EF_Core.Entities;

namespace EF_Core;

public class Concurrency
{
    public static void Run()
    {
        Utils.printTitle(title: "Insert ( using Context Concurrency )", color: ConsoleColor.Blue, width: 70);

        var context = new AppDbContext01();

        Task[] tasks =
        {
            Task.Factory.StartNew(() => Job1(context)),
            Task.Factory.StartNew(() => Job2(context)),
        };

        Task.WhenAll(tasks)
            .ContinueWith(t => Console.WriteLine("Job1 & Job2 executed concurrently"));
    }

    private static async Task Job1(AppDbContext01 context)
    {
        System.Console.WriteLine("in Job 1");

        Wallet walletToInsert = new Wallet { Holder = "Job1", Balance = 1 };

        context.Wallets.Add(walletToInsert);

        await context.SaveChangesAsync();

        Console.WriteLine($"Wallet {{ {walletToInsert} }} added successfully");
    }
    
    private static async Task Job2(AppDbContext01 context)
    {
        System.Console.WriteLine("in Job 2");

        Wallet walletToInsert = new Wallet { Holder = "Job2", Balance = 2 };

        context.Wallets.Add(walletToInsert);

        await context.SaveChangesAsync();

        Console.WriteLine($"Wallet {{ {walletToInsert} }} added successfully");
    }
}