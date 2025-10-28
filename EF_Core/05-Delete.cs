using Shared;
using EF_Core.Entities;

namespace EF_Core;

public class Delete
{
    public static void Run()
    {
        Utils.printTitle(title: "Delete", color: ConsoleColor.Blue, width: 70);

        Console.Write("Enter wallet id: ");
        int id = Convert.ToInt32(Console.ReadLine());

        using (var context = new AppDbContext())
        {
            Wallet? wallet = context.Wallets.SingleOrDefault(x => x.Id == id);

            // context.Remove(wallet!); // also worked
            context.Wallets.Remove(wallet!);

            context.SaveChanges();
            
            Console.WriteLine($"Wallet {{ {wallet} }} deleted successfully");
        }
    }
}