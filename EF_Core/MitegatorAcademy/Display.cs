using System;
using System.Collections.Generic;
using EF_Core.MitegatorAcademy.Data;
using EF_Core.MitegatorAcademy.Entities;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace EF_Core.MitegatorAcademy;

public abstract class Display
{
    public static void SectionCourse()
    {
        Utils.printTitle(title: "Display Sections with its Courses", color: ConsoleColor.Blue, width: 70);

        using (var context = new AppDbContext())
        {
            foreach (Section item in context.Sections.Include(x => x.Course))
            {
                Console.WriteLine($"Section: {item.SectionName}, " +
                                    $"Course: {item.Course.CourseName}");
            }
        }
    }

    public static void Course()
    {
        Utils.printTitle(title: "Display Courses", color: ConsoleColor.Blue, width: 70);

        using (var context = new AppDbContext())
        {
            context.Courses.Print("Courses");
        }
    }

    public static void CourseInDetails()
    {
        Utils.printTitle(title: "Display Course In Details", color: ConsoleColor.Blue, width: 70);
        // ConsoleTable.ExampleUsage02();

        ConsoleTable ct = new ConsoleTable(new List<int> { 4, 10, 9, 15, 15, 10, 10, 5, 5, 5, 5, 5, 5, 5 });
        ct.PrintLine(ConsoleTable.LineState.Top);
        ct.PrintRow("Id", "Course", "Section", "Instructor", "Schedule", "Start Time", "End Time", "SAT", "SUN", "MON", "TUE", "WED", "THU", "FRI");

        using (var context = new AppDbContext())
        {
            var sections = context.Sections
                                .Include(x => x.Course)
                                .Include(x => x.Instructor)
                                .Include(x => x.Schedule);

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
                    section.Schedule.Title.ToString(),
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