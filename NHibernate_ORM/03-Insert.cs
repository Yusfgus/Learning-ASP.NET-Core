
using NHibernate;
using NHibernate_ORM;
using NHibernate_ORM.Classes;
using Shared;

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

        using (ISession session = Session.CreateSession())
        {
            using (ITransaction transaction = session.BeginTransaction())
            {
                object obj = session.Save(walletToInsert);
                Console.WriteLine(obj);  // id
                Console.WriteLine(walletToInsert);

                transaction.Commit();
            }
        }
    }
}