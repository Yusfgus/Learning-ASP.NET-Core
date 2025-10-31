using Shared;
using EF_Core.CRUD.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using System;

namespace EF_Core.CRUD;

public class myDbContext
{
    public static void InternalDbContextConfig()
    {
        Utils.printTitle(title: "Select All Data ( using Internal DbContext configuration )", color: ConsoleColor.Blue, width: 70);

        using (var context = new AppDbContext01())
        {
            context.Wallets.Print("Wallets");
        }
    }

    public static void ExternalDbContextConfig()
    {
        Utils.printTitle(title: "Select All Data ( using External DbContext configuration )", color: ConsoleColor.Blue, width: 70);

        var optionsBuilder = new DbContextOptionsBuilder();

        optionsBuilder.UseSqlServer(Connection.connectionString);

        var options = optionsBuilder.Options;

        using (var context = new AppDbContext02(options))
        {
            context.Wallets.Print("Wallets");
        }
    }

    public static void DependencyInjection()
    {
        Utils.printTitle(title: "Select All Data ( using Dependency Injection )", color: ConsoleColor.Blue, width: 70);

        var services = new ServiceCollection();

        services.AddDbContext<AppDbContext01>();

        IServiceProvider serviceProvider = services.BuildServiceProvider();

        using (var context = serviceProvider.GetService<AppDbContext01>())
        {
            context.Wallets.Print("Wallets");
        }
    }
    
    public static void ContextFactory()
    {
        Utils.printTitle(title: "Select All Data ( using Context Factory )", color: ConsoleColor.Blue, width: 70);

        var services = new ServiceCollection();

        services.AddDbContextFactory<AppDbContext02>(options =>
            options.UseSqlServer(Connection.connectionString)
        );

        IServiceProvider serviceProvider = services.BuildServiceProvider();

        var contextFactory = serviceProvider.GetService<IDbContextFactory<AppDbContext02>>();

        using (var context = contextFactory!.CreateDbContext())
        {
            context.Wallets.Print("Wallets");
        }
    }

    public static void Concurrency()
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

    public static void ContextPooling()
    {
        Utils.printTitle(title: "Select All Data ( using Context Pooling )", color: ConsoleColor.Blue, width: 70);

        var services = new ServiceCollection();

        // max of 1024 instance
        services.AddDbContextPool<AppDbContext02>(options =>
            options.UseSqlServer(Connection.connectionString)
        );

        IServiceProvider serviceProvider = services.BuildServiceProvider();

        using (var context = serviceProvider.GetService<AppDbContext02>()!)
        {
            context.Wallets.Print("Wallets");
        }
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