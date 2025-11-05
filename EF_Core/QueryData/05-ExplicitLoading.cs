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

            Console.WriteLine(query.ToQueryString() + '\n');

            Console.WriteLine($"section: {section.SectionName}");
            Console.WriteLine($"--------------------");
            query.Print("Participants");
        }
    }
}