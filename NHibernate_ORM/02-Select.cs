using NHibernate;
using NHibernate_ORM.Classes;
using Shared;

namespace NHibernate_ORM;

public class Select
{
    public static void AllData()
    {
        Utils.printTitle(title: "Select ( All Data )", color: ConsoleColor.Blue, width: 70);

        using (ISession session = Session.CreateSession())
        {
            using (ITransaction transaction = session.BeginTransaction())
            {
                IQueryable<Wallet> wallets = session.Query<Wallet>();

                wallets.Print("All wallets");
            }
        }
    }

    public static void ById()
    {
        Utils.printTitle(title: "Select ( By ID )", color: ConsoleColor.Blue, width: 70);

        Console.Write("Enter wallet id: ");
        int id = Convert.ToInt32(Console.ReadLine());

        using (ISession session = Session.CreateSession(log: true))
        {
            using (ITransaction transaction = session.BeginTransaction())
            {
                // Wallet? wallet = session.Query<Wallet>().FirstOrDefault(x => x.Id == id);
                Wallet wallet = session.Get<Wallet>(id);

                Console.WriteLine(wallet);
            }
        }
    }
}