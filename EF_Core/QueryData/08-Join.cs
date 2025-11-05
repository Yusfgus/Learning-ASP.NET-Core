using System;
using System.Linq;
using EF_Core.QueryData.Data;
using EF_Core.QueryData.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Shared;

namespace EF_Core.QueryData;

public abstract class Join
{
    public static void Run()
    {
        Utils.printTitle(title: "Join", color: ConsoleColor.Blue, width: 70);

        string choice;
        bool flag = true;
        while (flag)
        {
            Console.Write("""
            Enter a choice:
            ───────────────
            1) Inner Join.
            2) Left Join.
            3) Cross Join.
            ───> 
            """);

            choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    InnerJoin();
                    break;
                case "2":
                    LeftJoin();
                    break;
                case "3":
                    CrossJoin();
                    break;
                default:
                    flag = false;
                    break;
            }

            Console.WriteLine("─────────────────────────────────\n");
        }
    }
    
    private static void InnerJoin()
    {
        Utils.printTitle("Inner Join");

        using (var context = new AppDbContext())
        {
            var result01 = from c in context.Courses.AsNoTracking()
                                  join s in context.Sections.AsNoTracking() on c.Id equals s.CourseId
                                  select new { c.CourseName, s.SectionName };

            // SELECT [c].[CourseName], [s].[SectionName]
            // FROM [Courses] AS [c]
            // INNER JOIN [Sections] AS [s] ON [c].[Id] = [s].[CourseId]

            var result02 = context.Courses.AsNoTracking()
                            .Join(context.Sections.AsNoTracking(),
                                c => c.Id,
                                s => s.CourseId,
                                (c, s) => new { c.CourseName, s.SectionName }
                            );

            result02.ToList().Print();
        }
    }
    
    private static void LeftJoin()
    {
        Utils.printTitle("Left Join");

        using (var context = new AppDbContext())
        {
            var result01 = from off in context.Offices.AsNoTracking()
                           join ins in context.Instructors.AsNoTracking()
                           on off.Id equals ins.OfficeId into OfficeVacancy
                           from ov in OfficeVacancy.DefaultIfEmpty()
                           select new
                           {
                               OfficeId = off.Id,
                               Name = off.OfficeName,
                               Instructor = ov != null ? ov.FName : "<<Empty>>"
                           };

            // SELECT [o].[Id] AS [OfficeId], [o].[OfficeName] AS [Name], CASE
            //     WHEN [i].[Id] IS NOT NULL THEN [i].[FName]
            //     ELSE '<<Empty>>'
            // END AS [Instructor]
            // FROM [Offices] AS [o]
            // LEFT JOIN [Instructors] AS [i] ON [o].[Id] = [i].[OfficeId]

            var result02 = context.Offices.AsNoTracking()
                            .GroupJoin(context.Instructors,
                                off => off.Id,
                                inst => inst.OfficeId,
                                (off, inst) => new { Office = off, Instructor = inst }
                            )
                            .SelectMany(ov => ov.Instructor.DefaultIfEmpty(),
                                (ov, inst) => new
                                {
                                    OfficeId = ov.Office.Id,
                                    Name = ov.Office.OfficeName,
                                    Instructor = inst != null ? inst.FName : "<<Empty>>"
                                }
                            );

            result02.ToList().Print();
        }
    }
    
    private static void CrossJoin()
    {
        Utils.printTitle("Cross Join");

        using (var context = new AppDbContext())
        {
            
        }
    }
}