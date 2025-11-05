using System;
using System.Linq;
using EF_Core.QueryData.Data;
using EF_Core.QueryData.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Shared;

namespace EF_Core.QueryData;

public abstract class SelectMany
{
    public static void Run()
    {
        Utils.printTitle(title: "Select Many", color: ConsoleColor.Blue, width: 70);

        using (var context = new AppDbContext())
        {
            // var frontendParticipants = from c in context.Courses
            //                            where c.CourseName.Contains("frontend")
            //                            from s in c.Sections
            //                            from p in s.Participants
            //                            select p.FullName;

            // SELECT [s0].[FName]
            // FROM [Courses] AS [c]
            // INNER JOIN [Sections] AS [s] ON [c].[Id] = [s].[CourseId]
            // INNER JOIN (
            //     SELECT [p].[FName], [e].[SectionId]
            //     FROM [Enrollments] AS [e]
            //     INNER JOIN [Participants] AS [p] ON [e].[ParticipantId] = [p].[Id]
            // ) AS [s0] ON [s].[Id] = [s0].[SectionId]
            // WHERE [c].[CourseName] LIKE '%frontend%'

            var frontendParticipants =
                context.Courses                                 // all courses
                .Where(c => c.CourseName.Contains("frontend"))  // frontend courses (angular, react)
                .SelectMany(c => c.Sections)                    // sections for each of these courses
                .SelectMany(s => s.Participants)                // participants for each of these sections
                .Select(p => p.FullName);                       // full name for each of these participants

            frontendParticipants.Print("Frontend Courses Participants");
        }
    }
}