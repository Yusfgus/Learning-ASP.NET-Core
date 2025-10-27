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
            1) Select all data.
            2) Select data by id.
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
                default:
                    flag = false;
                    break;
            }

            Console.WriteLine("──────────────────────────────────────────────────────────────────\n");
        }


        // Console.ReadKey();
    }
}