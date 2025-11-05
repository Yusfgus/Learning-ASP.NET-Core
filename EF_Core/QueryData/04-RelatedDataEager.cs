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

                var section = sectionQuery.FirstOrDefault();

                Console.WriteLine($"section: {section.SectionName} " +
                    $"[{section.Instructor.FName} " +
                    $"{section.Instructor.LName} " +
                    $"({section.Instructor.Office.OfficeName})]");
        }
    }

}