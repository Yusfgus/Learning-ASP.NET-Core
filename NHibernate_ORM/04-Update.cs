using NHibernate;
using NHibernate_ORM;
using NHibernate_ORM.Classes;
using Shared;

public class Update
{
    public static void Run()
    {
        Utils.printTitle(title: "Insert", color: ConsoleColor.Blue, width: 70);

        Wallet walletToUpdate = new Wallet();

        Console.Write("Enter wallet id: ");
        walletToUpdate.Id = Convert.ToInt32(Console.ReadLine());
        Console.Write("Enter new holder: ");
        walletToUpdate.Holder = Console.ReadLine();
        Console.Write("Enter new balance: ");
        walletToUpdate.Balance = Convert.ToInt32(Console.ReadLine());

        using (ISession session = Session.CreateSession())
        {
            using (ITransaction transaction = session.BeginTransaction())
            {
                Wallet wallet = session.Get<Wallet>(walletToUpdate.Id);

                wallet.Holder = walletToUpdate.Holder;
                wallet.Balance = walletToUpdate.Balance;

                session.Update(wallet);

                transaction.Commit();
            }
        }
    }
}