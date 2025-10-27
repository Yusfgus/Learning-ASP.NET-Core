using NHibernate;
using NHibernate_ORM;
using NHibernate_ORM.Classes;
using Shared;

public class Delete
{
    public static void Run()
    {
        Utils.printTitle(title: "Delete", color: ConsoleColor.Blue, width: 70);

        Wallet walletToDelete = new Wallet();

        Console.Write("Enter wallet id: ");
        walletToDelete.Id = Convert.ToInt32(Console.ReadLine());

        using (ISession session = Session.CreateSession())
        {
            using (ITransaction transaction = session.BeginTransaction())
            {
                Wallet wallet = session.Get<Wallet>(walletToDelete.Id);

                session.Delete(wallet);

                transaction.Commit();

                Console.WriteLine($"Wallet {{ {wallet} }} deleted successfully");
            }
        }
    }
}