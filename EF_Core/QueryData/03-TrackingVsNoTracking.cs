using System;
using System.Linq;
using EF_Core.QueryData.Data;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace EF_Core.QueryData;

public abstract class TrackingVsNoTracking
{
    public static void Run()
    {
        Utils.printTitle(title: "Tracking Vs No Tracking", color: ConsoleColor.Blue, width: 70);

        string choice;
        bool flag = true;
        while (flag)
        {
            Console.Write("""
            Enter a choice:
            ───────────────
            1) Change section name (As Tracking).
            2) Change section name (As No Tracking).
            Any other key to return to main menu.
            ───> 
            """);

            choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Tracking();
                    break;
                case "2":
                    NoTracking();
                    break;
                default:
                    flag = false;
                    break;
            }

            Console.WriteLine("─────────────────────────────────\n");
        }
    }

    private static void Tracking()
    {
        Utils.printTitle(title: "Change section name (As Tracking)", color: ConsoleColor.Blue, width: 70);

        Console.Write("Enter section id: ");
        int id = Convert.ToInt32(Console.ReadLine());

        using (var context = new AppDbContext())
        {
            var section = context.Sections.SingleOrDefault(x => x.Id == id);
            // var section = context.Sections.AsTracking.SingleOrDefault(x => x.Id == id);  // use when Context config is set to use No tracking

            Console.WriteLine($"Section name: {section.SectionName}");

            Console.Write("Enter new name: ");
            string newName = Console.ReadLine()!;

            section.SectionName = newName;

            context.SaveChanges();
        }

        using (var context = new AppDbContext())
        {
            var section = context.Sections.SingleOrDefault(x => x.Id == id);

            Console.WriteLine($"After being changed {section.SectionName}");
        }
    }

    private static void NoTracking()
    {
        Utils.printTitle(title: "Change section name (As No Tracking)", color: ConsoleColor.Blue, width: 70);

        Console.Write("Enter section id: ");
        int id = Convert.ToInt32(Console.ReadLine());

        using (var context = new AppDbContext())
        {
            var section = context.Sections.AsNoTracking().SingleOrDefault(x => x.Id == id);

            Console.WriteLine($"Section name: {section.SectionName}");

            Console.Write("Enter new name: ");
            string newName = Console.ReadLine()!;

            section.SectionName = newName;

            context.SaveChanges();
        }

        Console.WriteLine("After being changed");
        using (var context = new AppDbContext())
        {
            var section = context.Sections.SingleOrDefault(x => x.Id == id);
  
            Console.WriteLine($"After being changed {section.SectionName}");
        }
    }

}