using NHibernate;
using Shared;

namespace NHibernate_ORM;

class Program
{
    static void Main()
    {
        Utils.printTitle(title: "Hello World! ( NHibernate )", color: ConsoleColor.Red, width: 80);

        Session.GetConnectionString();

        string? choice;
        bool flag = true;
        while (flag)
        {
            Console.Write("""
            Enter a choice:
            1) Select all wallet.
            2) Select wallet by id.
            3) Insert wallet.
            4) Update wallet.
            5) Delete wallet.
            6) Transaction of Update.
            Any other key to exit.
            ---> 
            """);

            choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Select.AllData();
                    break;
                case "2":
                    Select.ById();
                    break;
                case "3":
                    Insert.Run();
                    break;
                case "4":
                    Update.Run();
                    break;
                case "5":
                    Delete.Run();
                    break;
                case "6":
                    Transaction.Run();
                    break;
                default:
                    flag = false;
                    break;
            }

            Console.WriteLine("──────────────────────────────────────────────────────────────────\n");
        }


        // Console.ReadKey();
    }
}