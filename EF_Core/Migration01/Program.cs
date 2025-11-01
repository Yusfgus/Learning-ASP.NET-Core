using System;
using EF_Core.Migration01.Data;
using Shared;

namespace EF_Core.Migration01;

class Program
{
    static void Main()
    {
        Utils.printTitle(title: "EF_Core ( Migration01 )", color: ConsoleColor.Red, width: 80);

        // Connection.SetConnectionString();
        
        string choice;
        bool flag = true;
        while (flag)
        {
            Console.Write("""
            Enter a choice:
            ───────────────
            1) Display Section with Course.
            Any other key to exit.
            ───> 
            """);

            choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Display.SectionCourse();
                    break;
                default:
                    flag = false;
                    break;
            }

            Console.WriteLine("──────────────────────────────────────────────────────────────────\n");
        }
    }
}