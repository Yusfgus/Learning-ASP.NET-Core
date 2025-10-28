using Shared;
using EF_Core.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace EF_Core;

public abstract class Select
{
    public static void AllData()
    {
        Utils.printTitle(title: "Select ( All Data )", color: ConsoleColor.Blue, width: 70);
        
        #region use external configuration DbContext
        var configuration = new ConfigurationBuilder()
           .AddJsonFile("appsettings.json")
           .Build();

        var constr = configuration.GetSection("constr").Value;

        var optionsBuilder = new DbContextOptionsBuilder();

        optionsBuilder.UseSqlServer(constr);

        var options = optionsBuilder.Options;
        #endregion

        using (var context = new AppDbContext02(options))
        {
            context.Wallets.Print("Wallets");
        }
    }

    public static void ById()
    {
        Utils.printTitle(title: "Select ( By ID )", color: ConsoleColor.Blue, width: 70);

        Console.Write("Enter wallet id: ");
        int id = Convert.ToInt32(Console.ReadLine());

        using (var context = new AppDbContext01())
        {
            // Wallet? wallet = context.Wallets.FirstOrDefault(x => x.Id == id);
            Wallet? wallet = context.Wallets.SingleOrDefault(x => x.Id == id);

            Console.WriteLine(wallet);
        }
    }

    public static void Where()
    {
        Utils.printTitle(title: "Select ( Where )", color: ConsoleColor.Blue, width: 70);

        using (var context = new AppDbContext01())
        {
            IQueryable<Wallet> result = context.Wallets.Where(x => x.Balance > 2000);

            result.Print("Wallets Where Balance > 2000");
        }
    }
}