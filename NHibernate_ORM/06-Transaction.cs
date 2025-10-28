using NHibernate;
using NHibernate_ORM.Classes;
using Shared;

namespace NHibernate_ORM;

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

        using (ISession session = Session.CreateSession())
        {
            using (ITransaction transaction = session.BeginTransaction())
            {
                Wallet fromWallet = session.Get<Wallet>(fromId);
                Wallet toWallet = session.Get<Wallet>(toId);

                fromWallet.Balance -= amount;
                toWallet.Balance += amount;

                session.Update(fromWallet);
                session.Update(toWallet);

                transaction.Commit();

                Console.WriteLine("Transaction of transfer completed successfully");
            }
        }
    }
}