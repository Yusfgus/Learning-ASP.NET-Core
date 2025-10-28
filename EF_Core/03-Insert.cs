using Shared;
using EF_Core.Entities;

namespace EF_Core;

public class Insert
{
    public static void Run()
    {
        Utils.printTitle(title: "Insert", color: ConsoleColor.Blue, width: 70);
        
        var walletToInsert = new Wallet();

        Console.Write("Enter wallet holder: ");
        walletToInsert.Holder = Console.ReadLine();

        Console.Write("Enter wallet balance: ");
        walletToInsert.Balance = Convert.ToInt32(Console.ReadLine());

        using (var context = new AppDbContext())
        {
            context.Wallets.Add(walletToInsert);

            context.SaveChanges();

            Console.WriteLine($"Wallet {{ {walletToInsert} }} added successfully");
        }
    }
}