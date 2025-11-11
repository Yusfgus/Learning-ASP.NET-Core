using System;
using System.Linq;
using EF_Core.QueryData.Data;
using EF_Core.QueryData.Entities;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace EF_Core.QueryData;

public abstract class ExplicitLoading
{
    public static void Run()
    {
        Utils.printTitle(title: "Explicit Loading", color: ConsoleColor.Blue, width: 70);

        Console.Write("Enter section id: ");
        int sectionId = Convert.ToInt32(Console.ReadLine());

        using (var context = new AppDbContext())
        {
            Section section = context.Sections.FirstOrDefault(x => x.Id == sectionId);

            IQueryable<Participant> query = context.Entry(section).Collection(x => x.Participants).Query();

            // Console.WriteLine(query.ToQueryString() + '\n');

            // SELECT [s0].[Id], [s0].[FName], [s0].[LName], [s].[Id], [s0].[SectionId], [s0].[ParticipantId], [s2].[SectionId], [s2].[ParticipantId], [s2].[Id], [s2].[CourseId], [s2].[InstructorId], [s2].[ScheduleId], [s2].[SectionName], [s2].[EndDate], [s2].[StartDate], [s2].[EndTime], [s2].[StartTime]
            // FROM [Sections] AS [s]
            // INNER JOIN (
            //     SELECT [p].[Id], [p].[FName], [p].[LName], [e].[SectionId], [e].[ParticipantId]
            //     FROM [Enrollments] AS [e]
            //     INNER JOIN [Participants] AS [p] ON [e].[ParticipantId] = [p].[Id]
            // ) AS [s0] ON [s].[Id] = [s0].[SectionId]
            // LEFT JOIN (
            //     SELECT [e0].[SectionId], [e0].[ParticipantId], [s1].[Id], [s1].[CourseId], [s1].[InstructorId], [s1].[ScheduleId], [s1].[SectionName], [s1].[EndDate], [s1].[StartDate], [s1].[EndTime], [s1].[StartTime]
            //     FROM [Enrollments] AS [e0]
            //     INNER JOIN [Sections] AS [s1] ON [e0].[SectionId] = [s1].[Id]
            //     WHERE [s1].[Id] = @__p_0
            // ) AS [s2] ON [s0].[Id] = [s2].[ParticipantId]
            // WHERE [s].[Id] = @__p_0
            // ORDER BY [s].[Id], [s0].[SectionId], [s0].[ParticipantId], [s0].[Id], [s2].[SectionId], [s2].[ParticipantId]

            Console.WriteLine($"section: {section.SectionName}");
            Console.WriteLine($"--------------------");
            query.Print("Participants");
        }
    }
}