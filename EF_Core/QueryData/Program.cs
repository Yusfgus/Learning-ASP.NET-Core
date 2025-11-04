using System;
using EF_Core.QueryData.Data;
using Shared;

namespace EF_Core.QueryData;

class Program
{
    static void Main()
    {
        Utils.printTitle(title: "EF_Core ( QueryData )", color: ConsoleColor.Red, width: 80);

        string choice;
        bool flag = true;
        while (flag)
        {
            Console.Write("""
            Enter a choice:
            ───────────────
            1) Client Vs Server Evaluation.
            2) Tracking Vs No Tracking.
            Any other key to exit.
            ───> 
            """);

            choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    ClientVsServerEvaluation.Run();
                    break;
                case "2":
                    TrackingVsNoTracking.Run();
                    break;
                default:
                    flag = false;
                    break;
            }

            Console.WriteLine("──────────────────────────────────────────────────────────────────\n");
        }
    }
}