using System;
using System.Linq;
using EF_Core.QueryData.Data;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace EF_Core.QueryData;

public abstract class ClientVsServerEvaluation
{
    public static void Run()
    {
        Utils.printTitle(title: "Client Vs Server Evaluation", color: ConsoleColor.Blue, width: 70);

        string choice;
        bool flag = true;
        while (flag)
        {
            Console.Write("""
            Enter a choice:
            ───────────────
            1) Select Sections with Course Id (Normal).
            2) Select Sections with Course Id (Use External Function).
            3) Select Sections with Course Id (Use Built in Function).
            Any other key to return to main menu.
            ───> 
            """);

            choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Run01();
                    break;
                case "2":
                    Run02();
                    break;
                case "3":
                    Run03();
                    break;
                default:
                    flag = false;
                    break;
            }

            Console.WriteLine("─────────────────────────────────\n");
        }
    }

    private static void Run01()
    {
        Utils.printTitle(title: "Select Sections with Course Id", color: ConsoleColor.Blue, width: 70);

        using (var context = new AppDbContext())
        {
            var courseId = 1;

            var result = context.Sections
                .Where(x => x.CourseId == courseId)
                .Select(x => new
                {
                    Id = x.Id,
                    Section = x.SectionName
                });

            //DECLARE @__courseId_0 int = 1;
            //SELECT[s].[Id], [s].[SectionName] AS[Section]
            //FROM[Sections] AS[s]
            //WHERE[s].[CourseId] = @__courseId_0

            Console.WriteLine(result.ToQueryString());

            foreach (var item in result)
            {
                Console.WriteLine($"{item.Id} {item.Section}");
            }
        }
    }

    private static void Run02()
    {
        Utils.printTitle(title: "Use External Function (TotalDays)", color: ConsoleColor.Blue, width: 70);

        using (var context = new AppDbContext())
        {
            var courseId = 1;

            var result = context.Sections
                .Where(x => x.CourseId == courseId)
                .Select(x => new
                {
                    Id = x.Id,
                    Section = x.SectionName,
                    TotalDays = TotalDays(x.DateRange.StartDate, x.DateRange.EndDate)
                });

            //  SELECT [s].[Id], [s].[SectionName], [s].[StartDate], [s].[EndDate]
            //  FROM [Sections] AS [s]
            //  WHERE [s].[CourseId] = @__courseId_0

           Console.WriteLine(result.ToQueryString());

            foreach (var item in result)
            {
                Console.WriteLine($"{item.Id} {item.Section} ({item.TotalDays})");
            }

            Console.WriteLine("\nNote: it didn't work when i used Delegate");
        }
    }

    private static int TotalDays(DateOnly startDate, DateOnly endDate) => endDate.DayNumber - startDate.DayNumber; // 0001-01-01

    private static void Run03()
    {
        Utils.printTitle(title: "Use Built in Function (Substring)", color: ConsoleColor.Blue, width: 70);

        using (var context = new AppDbContext())
        {
            var courseId = 1;

            var result = context.Sections
                .Where(x => x.CourseId == courseId)
                .Select(x => new
                {
                    Id = x.Id,
                    Section = x.SectionName.Substring(4)
                });

            //  SELECT [s].[Id], [s].[SectionName], [s].[StartDate], [s].[EndDate]
            //  FROM [Sections] AS [s]
            //  WHERE [s].[CourseId] = @__courseId_0

            Console.WriteLine(result.ToQueryString());

            foreach (var item in result)
            {
                Console.WriteLine($"{item.Id} {item.Section}");
            }
        }
    }

}