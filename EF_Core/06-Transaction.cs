using Shared;
using EF_Core.Entities;
using System.Linq;
using System;

namespace EF_Core;

public class Transaction
{
    public static void Run()
    {
        Utils.printTitle(title: "Transaction", color: ConsoleColor.Blue, width: 70);

        Console.Write("Enter from wallet id: ");
        int fromId = Convert.ToInt32(Console.ReadLine());
        Console.Write("Enter to wallet id: ");
        int toId = Convert.ToInt32(Console.ReadLine());
        Console.Write("Enter amount to transfer: ");
        decimal amount = Convert.ToDecimal(Console.ReadLine());

        using (var context = new AppDbContext01())
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                Wallet fromWallet = context.Wallets.SingleOrDefault(x => x.Id == fromId);
                Wallet toWallet = context.Wallets.SingleOrDefault(x => x.Id == toId);

                Console.WriteLine(fromWallet);
                Console.WriteLine(toWallet);

                fromWallet!.Balance -= amount;
                context.SaveChanges();

                toWallet!.Balance += amount;
                context.SaveChanges();

                transaction.Commit();

                Console.WriteLine("Transaction of transfer completed successfully");

                Console.WriteLine(fromWallet);
                Console.WriteLine(toWallet);
            }
        }
    }

}