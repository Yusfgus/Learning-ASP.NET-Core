using Shared;
using EF_Core.CRUD.Entities;
using System;
using System.Linq;

namespace EF_Core.CRUD;

public class Update
{
    public static void Run01()
    {
        Utils.printTitle(title: "Update ( using .Update() )", color: ConsoleColor.Blue, width: 70);

        Wallet walletToUpdate = new Wallet();

        Console.Write("Enter wallet id: ");
        walletToUpdate.Id = Convert.ToInt32(Console.ReadLine());
        Console.Write("Enter new holder: ");
        walletToUpdate.Holder = Console.ReadLine();
        Console.Write("Enter new balance: ");
        walletToUpdate.Balance = Convert.ToInt32(Console.ReadLine());

        using (var context = new AppDbContext01())
        {
            // context.Update(walletToUpdate);  // also worked
            context.Wallets.Update(walletToUpdate);

            context.SaveChanges();

            Console.WriteLine($"Wallet {{ {walletToUpdate} }} updated successfully");
        }
    }
    
    public static void Run02()
    {
        Utils.printTitle(title: "Update ( using tracking )", color: ConsoleColor.Blue, width: 70);

        Wallet walletToUpdate = new Wallet();

        Console.Write("Enter wallet id: ");
        walletToUpdate.Id = Convert.ToInt32(Console.ReadLine());
        Console.Write("Enter new holder: ");
        walletToUpdate.Holder = Console.ReadLine();
        Console.Write("Enter new balance: ");
        walletToUpdate.Balance = Convert.ToInt32(Console.ReadLine());

        using (var context = new AppDbContext01())
        {
            Wallet wallet = context.Wallets.SingleOrDefault(x => x.Id == walletToUpdate.Id);

            wallet!.Holder = walletToUpdate.Holder;
            wallet!.Balance = walletToUpdate.Balance;

            context.SaveChanges();
            
            Console.WriteLine($"Wallet {{ {walletToUpdate} }} updated successfully");
        }
    }
}