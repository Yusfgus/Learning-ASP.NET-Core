using NHibernate;
using NHibernate_ORM.Classes;
using Shared;

namespace NHibernate_ORM;

public class Delete
{
    public static void Run()
    {
        Utils.printTitle(title: "Delete", color: ConsoleColor.Blue, width: 70);

        Console.Write("Enter wallet id: ");
        int id = Convert.ToInt32(Console.ReadLine());

        using (ISession session = Session.CreateSession())
        {
            using (ITransaction transaction = session.BeginTransaction())
            {
                Wallet wallet = session.Get<Wallet>(id);

                session.Delete(wallet);

                transaction.Commit();

                Console.WriteLine($"Wallet {{ {wallet} }} deleted successfully");
            }
        }
    }
}