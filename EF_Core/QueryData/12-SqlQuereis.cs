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
                .FromSql($"SELECT * FROM dbo.Courses Where Id = {1}")
                .FirstOrDefault();

            Console.WriteLine($"{c1.CourseName} ({c1.HoursToComplete})");
            
            //====================================================================================

            Console.WriteLine("\nFromSqlInterpolated() without Parameters ( safe ✅ )");
            var c2 = context.Courses
                .FromSqlInterpolated($"SELECT * FROM dbo.Courses Where Id = {1}")
                .FirstOrDefault();

            Console.WriteLine($"{c2.CourseName} ({c2.HoursToComplete})");

            //====================================================================================

            Console.WriteLine("\nFromSqlRaw() without Parameters ( unsafe ❌ )");
            // var courseId = "1; DELETE FROM dbo.Courses";
            var c3 = context.Courses
                .FromSqlRaw($"SELECT * FROM dbo.Courses Where Id = {1}")
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
    
}