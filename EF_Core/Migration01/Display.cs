using System;
using System.Collections.Generic;
using EF_Core.Migration01.Data;
using EF_Core.Migration01.Entities;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace EF_Core.Migration01;

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
            foreach (Course course in context.Courses.Include(c => c.Sections)
                                                        .ThenInclude(sec => sec.Instructor)
                                                    .Include(c => c.Sections)
                                                        .ThenInclude(sec => sec.SectionSchedules)
                                                        .ThenInclude(s => s.Schedule)
            )
            {
                foreach (Section section in course.Sections)
                {
                    Instructor? instructor = section.Instructor;
                    string instructorName = instructor is not null? $"{instructor!.FName} {instructor!.LName}": String.Empty;

                    foreach (var sectionSchedule in section.SectionSchedules)
                    {
                        ct.PrintLine(ConsoleTable.LineState.Middle);
                        ct.PrintRow(
                            course.Id.ToString()!,
                            course.CourseName!,
                            section.SectionName!,
                            instructorName,
                            sectionSchedule.Schedule.Title!,
                            sectionSchedule.StartTime.ToString(),
                            sectionSchedule.EndTime.ToString(),
                            sectionSchedule.Schedule.SUN ? "✔️" : "",
                            sectionSchedule.Schedule.MON ? "✔️" : "",
                            sectionSchedule.Schedule.TUE ? "✔️" : "",
                            sectionSchedule.Schedule.WED ? "✔️" : "",
                            sectionSchedule.Schedule.THU ? "✔️" : "",
                            sectionSchedule.Schedule.FRI ? "✔️" : "",
                            sectionSchedule.Schedule.SAT ? "✔️" : ""
                        );
                    }
                }
                
            }
        }
        ct.PrintLine(ConsoleTable.LineState.Bottom);
    }
}