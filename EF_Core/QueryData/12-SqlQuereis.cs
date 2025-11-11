using System;
using System.Linq;
using EF_Core.QueryData.Data;
using EF_Core.QueryData.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Shared;

namespace EF_Core.QueryData;

public abstract class SqlQueries
{
    public static void Run()
    {
        Utils.printTitle(title: "Sql Queries", color: ConsoleColor.Blue, width: 70);

        string choice;
        bool flag = true;
        while (flag)
        {
            Console.Write("""
            Enter a choice:
            ───────────────
            1) Sql Query.
            2) Sql Query Parameters.
            3) Stored Procedures.
            4) View.
            5) Scaler Valued Function.
            6) Table Valued Function.
            7) Global Query Filter.
            ───> 
            """);

            choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    SqlQuery();
                    break;
                case "2":
                    SqlQueryParameters();
                    break;
                case "3":
                    StoredProcedures();
                    break;
                case "4":
                    View();
                    break;
                case "5":
                    ScalerValuedFunction();
                    break;
                case "6":
                    TableValuedFunction();
                    break;
                case "7":
                    GlobalQueryFilter();
                    break;
                default:
                    flag = false;
                    break;
            }

            Console.WriteLine("─────────────────────────────────\n");
        }
    }
    
    private static void SqlQuery()
    {
        Utils.printTitle("Sql Query");

        using (var context = new AppDbContext())
        {
            // FromSql +ef 7.0
            // FromSqlInterpolated ef 3.0
            // FromSqlRaw ef 3.0

            Console.WriteLine("FromSql()");
            var courses01 =
                context.Courses.FromSql($"SELECT * FROM dbo.Courses")
                .ToList();

            Console.WriteLine("\nFromSqlInterpolated()");
            var courses02 =
                context.Courses.FromSqlInterpolated($"SELECT * FROM dbo.Courses")
                .ToList();

            Console.WriteLine("\nFromSqlRaw()");
            var courses03 =
                context.Courses.FromSqlRaw("SELECT * FROM dbo.Courses")
                .ToList();
        }
    }

    private static void SqlQueryParameters()
    {
        Utils.printTitle("Sql Query Parameters");

        using (var context = new AppDbContext())
        {
            Console.WriteLine("FromSql() without Parameters ( safe ✅ )");
            var c1 = context.Courses
                .FromSql($"SELECT * FROM dbo.Courses Where Id = {1}") // passed as normal FormattableString
                .FirstOrDefault();

            Console.WriteLine($"{c1.CourseName} ({c1.HoursToComplete})");

            //====================================================================================

            Console.WriteLine("\nFromSqlInterpolated() without Parameters ( safe ✅ )");
            var c2 = context.Courses
                .FromSqlInterpolated($"SELECT * FROM dbo.Courses Where Id = {1}") // passed as normal FormattableString
                .FirstOrDefault();

            Console.WriteLine($"{c2.CourseName} ({c2.HoursToComplete})");

            //====================================================================================

            Console.WriteLine("\nFromSqlRaw() without Parameters ( unsafe ❌ )");
            // var courseId = "1; DELETE FROM dbo.Courses";
            var c3 = context.Courses
                .FromSqlRaw($"SELECT * FROM dbo.Courses Where Id = {1}") // passed as normal string
                .FirstOrDefault();

            //====================================================================================

            Console.WriteLine("\nFromSqlRaw() with Parameters ( safe ✅ )");
            var courseIdParam = new SqlParameter("@courseId", 1);
            var c4 = context.Courses
                .FromSqlRaw("SELECT * FROM dbo.Courses Where Id = @courseId", courseIdParam)
                .FirstOrDefault();

            Console.WriteLine($"{c4.CourseName} ({c4.HoursToComplete})");
        }
    }

    private static void StoredProcedures()
    {
        Utils.printTitle("Stored Procedures");

        using (var context = new AppDbContext())
        {
            var startDateParam = new SqlParameter("@StartDate", System.Data.SqlDbType.Date)
            {
                Value = new DateTime(2023, 01, 01)
            };
            var endDateParam = new SqlParameter("@EndDate", System.Data.SqlDbType.Date)
            {
                Value = new DateTime(2023, 06, 30)
            };

            var sections = context.SectionDetails
                .FromSql($"Exec dbo.sp_GetSectionWithinDateRange {startDateParam}, {endDateParam}")
                .ToList();

            // var sections = context.SectionDetails
            //     .FromSql($"Exec dbo.sp_GetSectionWithinDateRange {new DateTime(2023, 01, 01)}, {new DateTime(2023, 06, 30)}")
            //     .ToList();

            sections.Print("Section");
        }
    }

    private static void View()
    {
        Utils.printTitle("View");

        using (var context = new AppDbContext())
        {
            // has a entity class, a configurations class and a DbSet in the context (like normal tables)
            var courseOverviews = context.CourseOverviews
                                    .ToList();

            courseOverviews.Print("Course Overviews");
        }
    }

    private static void ScalerValuedFunction()
    {
        Utils.printTitle("Scaler Valued Function");

        var startDate = new DateTime(2023, 09, 24);
        var endDate = new DateTime(2023, 12, 26);
        var startTime = new TimeSpan(08, 00, 00);
        var endTime = new TimeSpan(11, 00, 00);

        using (var context = new AppDbContext())
        {
            var result = context.Instructors
                        .Select(i => new
                        {
                            i.Id,
                            i.FullName,
                            DateRange = $"{startDate.ToShortDateString()}-{endDate:d}",
                            TimeRange = $"{startTime.ToString("hh\\:mm")}-{endDate:hh\\:mm}",
                            // from database function
                            Status = context.GetInstructorAvailability(i.Id, startDate, endDate, startTime, endTime)
                        })
                        .ToList();

            foreach (var item in result)
            {
                Console.WriteLine(
                    $"[{item.Id}]\t{item.FullName,-20}\t{item.DateRange}\t{item.TimeRange}\t{item.Status}");
            }
        }
    }

    private static void TableValuedFunction()
    {
        Utils.printTitle("Table Valued Function");

        using (var context = new AppDbContext())
        {
            foreach(var section in context.GetSectionsExceedingParticipantCount(21))
            {
                Console.WriteLine(section);
            }
        }
    }

    private static void GlobalQueryFilter()
    {
        Utils.printTitle("Global Query Filter");

        Console.WriteLine("""
        Steps:
        1) Navigate to Entity Configuration class. ex: SectionConfiguration
        2) Navigate to Configure() function.
        3) add: builder.HasQueryFilter(x => condition(x.property) );
            ex: builder.HasQueryFilter(x => x.Id >= 5);
        """);
    }

}