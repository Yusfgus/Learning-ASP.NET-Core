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
            5) Update wallet using tracking.
            6) Delete wallet.
            7) Transaction of Update.
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
                    Update.Run01();
                    break;
                case "5":
                    Update.Run02();
                    break;
                case "6":
                    Delete.Run();
                    break;
                default:
                    flag = false;
                    break;
            }

            Console.WriteLine("──────────────────────────────────────────────────────────────────\n");
        }
    }
}