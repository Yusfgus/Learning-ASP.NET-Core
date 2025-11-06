using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EF_Core.QueryData.Data;
using EF_Core.QueryData.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Shared;

namespace EF_Core.QueryData;

public abstract class Pagination
{
    public static void Run()
    {
        Utils.printTitle(title: "Pagination", color: ConsoleColor.Blue, width: 70);

        ConsoleTable ct = new ConsoleTable(new List<int> { 4, 12, 9, 15, 15, 10, 10, 5, 5, 5, 5, 5, 5, 5 });

        Console.Write("Enter Page Number: ");
        int PageNumber = Convert.ToInt32(Console.ReadLine());
        int PageSize = 20;

        using (var context = new AppDbContext())
        {
            var sections = context.Sections
                            .Include(x => x.Course)
                            .Include(x => x.Instructor)
                            .Include(x => x.Schedule)
                            .Skip((PageNumber - 1) * PageSize)
                            .Take(PageSize)
                            .ToList();

            ct.PrintLine(ConsoleTable.LineState.Top);
            ct.PrintRow("Id", "Course", "Section", "Instructor", "Schedule", "Start Time", "End Time", "SAT", "SUN", "MON", "TUE", "WED", "THU", "FRI");
            foreach (Section section in sections)
            {
                Instructor? instructor = section.Instructor;
                string instructorName = instructor is not null ? $"{instructor!.FName} {instructor!.LName}" : String.Empty;

                ct.PrintLine(ConsoleTable.LineState.Middle);
                ct.PrintRow(
                    section.Id.ToString()!,
                    section.Course.CourseName!,
                    section.SectionName!,
                    instructorName,
                    section.Schedule.ScheduleType.ToString(),
                    section.TimeSlot.StartTime.ToString("hh\\:mm"),
                    section.TimeSlot.EndTime.ToString("hh\\:mm"),
                    section.Schedule.SUN ? "✔️" : "",
                    section.Schedule.MON ? "✔️" : "",
                    section.Schedule.TUE ? "✔️" : "",
                    section.Schedule.WED ? "✔️" : "",
                    section.Schedule.THU ? "✔️" : "",
                    section.Schedule.FRI ? "✔️" : "",
                    section.Schedule.SAT ? "✔️" : ""
                );

            }
        }
        ct.PrintLine(ConsoleTable.LineState.Bottom);
    }
}