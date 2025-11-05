using System;
using System.Linq;
using EF_Core.QueryData.Data;
using EF_Core.QueryData.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Shared;

namespace EF_Core.QueryData;

public abstract class SplitQueries
{
    public static void Run()
    {
        Utils.printTitle(title: "Split Queries", color: ConsoleColor.Blue, width: 70);

        string choice;
        bool flag = true;
        while (flag)
        {
            Console.Write("""
            Enter a choice:
            ───────────────
            1) Without proper projection.
            2) With proper projection (Select).
            3) Using AsSplitQuery().
            4) Using QuerySplittingBehavior.SplitQuery (Global for project).
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
                case "4":
                    Run04();
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
        Utils.printTitle("Without Proper Projection");

        using (var context = new AppDbContext())
        {
            var coursesProjection = context.Courses.AsNoTracking()
                .Include(c => c.Sections)
                .Include(c => c.Reviews)
                .ToList();
        }
    }
    
    private static void Run02()
    {
        Utils.printTitle("With Proper Projection");

        using (var context = new AppDbContext())
        {
            var coursesProjection = context.Courses.AsNoTracking()
                .Select(c => new
                {
                    CourseId = c.Id,
                    c.CourseName,
                    Hours = c.HoursToComplete,
                    Sections = c.Sections.Select(s => new
                    {
                        SectionId = s.Id,
                        s.SectionName,
                        DateRange = s.DateRange.ToString(),
                        TimeSlot = s.TimeSlot.ToString()
                    }),
                    Reviews = c.Reviews.Select(r => new
                    {
                        r.Feedback,
                        r.CreatedAt,
                    })
                }).ToList();
        }
    }

    private static void Run03()
    {
        Utils.printTitle("Using AsSplitQuery()");

        using (var context = new AppDbContext())
        {
            var coursesProjection = context.Courses.AsNoTracking()
                .Include(c => c.Sections)
                .Include(c => c.Reviews)
                .AsSplitQuery()
                .ToList();
        }
    }
    
    private static void Run04()
    {
        Utils.printTitle("Using QuerySplittingBehavior.SplitQuery (Global for project)");

        Console.WriteLine("""
        In AppDbContext -> OnConfiguring(): Add
        optionsBuilder.UseSqlServer(connectionString, o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)) 
        """);
    }
}