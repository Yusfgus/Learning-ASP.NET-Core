using Shared;
using EF_Core.Data;
using EF_Core.Entities;

namespace EF_Core;

public abstract class Select
{
    public static void AllData()
    {
        Utils.printTitle(title: "Select ( All Data )", color: ConsoleColor.Blue, width: 70);

        using (var context = new AppDbContext())
        {
            context.Wallets.Print("Wallets");
        }
    }
    
    public static void ById()
    {
        Utils.printTitle(title: "Select ( By ID )", color: ConsoleColor.Blue, width: 70);

        Console.Write("Enter wallet id: ");
        int id = Convert.ToInt32(Console.ReadLine());

        using (var context = new AppDbContext())
        {
            Wallet? wallet = context.Wallets.FirstOrDefault(x => x.Id == id);

            Console.WriteLine(wallet);
        }
    }
}