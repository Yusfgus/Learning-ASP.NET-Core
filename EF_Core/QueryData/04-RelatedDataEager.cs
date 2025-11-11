using System;
using System.Linq;
using EF_Core.QueryData.Data;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace EF_Core.QueryData;

public abstract class RelatedDataEager
{
    public static void Run()
    {
        Utils.printTitle(title: "Related Data Eager", color: ConsoleColor.Blue, width: 70);

        string choice;
        bool flag = true;
        while (flag)
        {
            Console.Write("""
            Enter a choice:
            ───────────────
            1) Select Section with its Participants (Include).
            2) Select Section with its Instructor and his Office (ThenInclude).
            Any other key to return to main menu.
            ───> 
            """);

            choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Include();
                    break;
                case "2":
                    WithInclude();
                    break;
                default:
                    flag = false;
                    break;
            }

            Console.WriteLine("─────────────────────────────────\n");
        }
    }

    private static void Include()
    {
        Utils.printTitle(title: "Select Section with Participants (Include)");

        Console.Write("Enter section id: ");
        int sectionId = Convert.ToInt32(Console.ReadLine());

        using (var context = new AppDbContext())
        {
            var sectionQuery = context.Sections
                   .Include(x => x.Participants)
                   .Where(x => x.Id == sectionId);

            // Console.WriteLine(sectionQuery.ToQueryString() + '\n');

            // SELECT [s0].[Id], [s0].[CourseId], [s0].[InstructorId], [s0].[ScheduleId], [s0].[SectionName], [s0].[EndDate], [s0].[StartDate], [s0].[EndTime], [s0].[StartTime], [s1].[SectionId], [s1].[ParticipantId], [s1].[Id], [s1].[FName], [s1].[LName]
            // FROM (
            //     SELECT TOP(1) [s].[Id], [s].[CourseId], [s].[InstructorId], [s].[ScheduleId], [s].[SectionName], [s].[EndDate], [s].[StartDate], [s].[EndTime], [s].[StartTime]
            //     FROM [Sections] AS [s]
            //     WHERE [s].[Id] = @__sectionId_0
            // ) AS [s0]
            // LEFT JOIN (
            //     SELECT [e].[SectionId], [e].[ParticipantId], [p].[Id], [p].[FName], [p].[LName]
            //     FROM [Enrollments] AS [e]
            //     INNER JOIN [Participants] AS [p] ON [e].[ParticipantId] = [p].[Id]
            // ) AS [s1] ON [s0].[Id] = [s1].[SectionId]
            // ORDER BY [s0].[Id], [s1].[SectionId], [s1].[ParticipantId]

            var section = sectionQuery.FirstOrDefault();

            Console.WriteLine($"section: {section.SectionName}");
            Console.WriteLine($"--------------------");
            section.Participants.Print("Participants");
        }
    }

    private static void WithInclude()
    {
        Utils.printTitle(title: "Select Section with Instructor and his Office (ThenInclude)");

        Console.Write("Enter section id: ");
        int sectionId = Convert.ToInt32(Console.ReadLine());

        using (var context = new AppDbContext())
        {
            var sectionQuery = context.Sections
                  .Include(x => x.Instructor)
                  .ThenInclude(x => x.Office)
                  .Where(x => x.Id == sectionId);

                // Console.WriteLine(sectionQuery.ToQueryString() + '\n');
                
                // SELECT TOP(1) [s].[Id], [s].[CourseId], [s].[InstructorId], [s].[ScheduleId], [s].[SectionName], [s].[EndDate], [s].[StartDate], [s].[EndTime], [s].[StartTime], [i].[Id], [i].[FName], [i].[LName], [i].[OfficeId], [o].[Id], [o].[OfficeLocation], [o].[OfficeName]
                // FROM [Sections] AS [s]
                // LEFT JOIN [Instructors] AS [i] ON [s].[InstructorId] = [i].[Id]
                // LEFT JOIN [Offices] AS [o] ON [i].[OfficeId] = [o].[Id]
                // WHERE [s].[Id] = @__sectionId_0

                var section = sectionQuery.FirstOrDefault();

                Console.WriteLine($"section: {section.SectionName} " +
                    $"[{section.Instructor.FName} " +
                    $"{section.Instructor.LName} " +
                    $"({section.Instructor.Office.OfficeName})]");
        }
    }

}