using System;
using System.Collections;
using System.Linq;
using EF_Core.QueryData.Data;
using EF_Core.QueryData.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Shared;

namespace EF_Core.QueryData;

public abstract class GroupBy
{
    public static void Run()
    {
        Utils.printTitle(title: "Group By", color: ConsoleColor.Blue, width: 70);

        using (var context = new AppDbContext())
        {
            // var instructorSections = from s in context.Sections.AsNoTracking()
            //                          group s by s.Instructor into g
            //                          select new
            //                          {
            //                              Instructor = g.Key,
            //                              //  Sections = g.ToList()
            //                              TotalSections = g.Count()
            //                          };

            // SELECT [i].[Id], [i].[FName], [i].[LName], [i].[OfficeId], COUNT(*) AS [TotalSections]
            // FROM [Sections] AS [s]
            // LEFT JOIN [Instructors] AS [i] ON [s].[InstructorId] = [i].[Id]
            // GROUP BY [i].[Id], [i].[FName], [i].[LName], [i].[OfficeId]

            var instructorSections = context.Sections
                                    .GroupBy(s => s.Instructor,
                                            (i, g) => new {
                                                Instructor = i,
                                                //  Sections = g.ToList()
                                                TotalSections = g.Count()
                                            }
                                     );
            
            foreach(var item in instructorSections)
            {
                // Console.WriteLine(item.Instructor.FullName);
                // foreach (var s in item.Sections)
                //     Console.WriteLine('\t' + s.SectionName);

                Console.WriteLine($"{item.Instructor.FullName}: {item.TotalSections}");
                
                Console.WriteLine("────────────────");
            }
        }
    }
}