using System;
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
            foreach(Section item in context.Sections.Include(x => x.Course))
            {
                Console.WriteLine($"Section: {item.SectionName}, " +
                                    $"Course: {item.Course.CourseName}");
            }
        }
    }
}