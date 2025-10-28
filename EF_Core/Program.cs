using Shared;

namespace EF_Core;

class Program
{
    static void Main()
    {
        Utils.printTitle(title: "Hello World! ( EF_Core )", color: ConsoleColor.Red, width: 80);
        
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
                    Console.WriteLine("Coming soon...");
                    break;
                case "4":
                    Console.WriteLine("Coming soon...");
                    break;
                case "5":
                    Console.WriteLine("Coming soon...");
                    break;
                case "6":
                    Console.WriteLine("Coming soon...");
                    break;
                default:
                    flag = false;
                    break;
            }

            Console.WriteLine("──────────────────────────────────────────────────────────────────\n");
        }
    }
}