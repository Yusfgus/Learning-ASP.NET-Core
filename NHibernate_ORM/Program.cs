using Shared;

namespace NHibernate_ORM;

class Program
{
    static void Main()
    {
        Utils.printTitle(title: "Hello World! ( NHibernate )", color: ConsoleColor.Red, width: 80);

        Session.CreateSession();

        string? choice;
        bool flag = true;
        while (flag)
        {
            Console.Write("""
            Enter a choice:
            1) .
            Any other key to exit.
            ---> 
            """);

            choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    
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