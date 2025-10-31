using System;
using EF_Core.Migration.Data;
using Shared;

namespace EF_Core.Migration;

class Program
{
    static void Main()
    {
        Utils.printTitle(title: "EF_Core ( Migration )", color: ConsoleColor.Red, width: 80);

        // Connection.SetConnectionString();
        
        string choice;
        bool flag = true;
        while (flag)
        {
            Console.Write("""
            Enter a choice:
            ───────────────
            1) .
            Any other key to exit.
            ───> 
            """);

            choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.WriteLine("Not implemented yet");
                    break;
                default:
                    flag = false;
                    break;
            }

            Console.WriteLine("──────────────────────────────────────────────────────────────────\n");
        }
    }
}