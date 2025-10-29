using Shared;

namespace EF_Core;

class Program
{
    static void Main()
    {
        Utils.printTitle(title: "Hello World! ( EF_Core )", color: ConsoleColor.Red, width: 80);

        Connection.SetConnectionString();
        
        string? choice;
        bool flag = true;
        while (flag)
        {
            Console.Write("""
            Enter a choice:
            ───────────────
            1) Select all wallet.
            2) Select wallet by id.
            3) Select with Where.
            4) Insert wallet.
            5) Update wallet.
            6) Update wallet using tracking.
            7) Delete wallet.
            8) Transaction of Update.
            Any other key to exit.
            ───> 
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
                    Select.Query();
                    break;
                case "4":
                    Insert.Run();
                    break;
                case "5":
                    Update.Run01();
                    break;
                case "6":
                    Update.Run02();
                    break;
                case "7":
                    Delete.Run();
                    break;
                case "8":
                    Transaction.Run();
                    break;
                default:
                    flag = false;
                    break;
            }

            Console.WriteLine("──────────────────────────────────────────────────────────────────\n");
        }
    }
}