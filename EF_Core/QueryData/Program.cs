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
            3) Related Data Eager (Include & ThenInclude).
            4) Explicit Loading.
            5) Lazy Loading.
            6) Split Queries (Specify columns).
            7) Join.
            8) Select Many.
            9) Group By.
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
                case "3":
                    RelatedDataEager.Run();
                    break;
                case "4":
                    ExplicitLoading.Run();
                    break;
                case "5":
                    LazyLoading.Run();
                    break;
                case "6":
                    SplitQueries.Run();
                    break;
                case "7":
                    Join.Run();
                    break;
                case "8":
                    SelectMany.Run();
                    break;
                case "9":
                    GroupBy.Run();
                    break;
                default:
                    flag = false;
                    break;
            }

            Console.WriteLine("──────────────────────────────────────────────────────────────────\n");
        }
    }
}