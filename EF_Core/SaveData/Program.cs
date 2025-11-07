using System;
using Shared;

namespace EF_Core.SaveData;

class Program
{
    static void Main()
    {
        Utils.printTitle(title: "EF_Core ( SaveData )", color: ConsoleColor.Red, width: 80);

        string choice;
        bool flag = true;
        while (flag)
        {
            Console.Write("""
            Enter a choice:
            ───────────────
            1) Basic Save.
            2) Change Tracking.
            Any other key to exit.
            ───> 
            """);

            choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    BasicSave.Run();
                    break;
                case "2":
                    ChangeTracking.Run();
                    break;
                default:
                    flag = false;
                    break;
            }

            Console.WriteLine("──────────────────────────────────────────────────────────────────\n");
        }
    }
}