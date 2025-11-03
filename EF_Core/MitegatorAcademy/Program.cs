using System;
using EF_Core.MitegatorAcademy.Data;
using Shared;

namespace EF_Core.MitegatorAcademy;

class Program
{
    static void Main()
    {
        Utils.printTitle(title: "EF_Core ( MitegatorAcademy )", color: ConsoleColor.Red, width: 80);

        // Connection.SetConnectionString();
        
        string choice;
        bool flag = true;
        while (flag)
        {
            Console.Write("""
            Enter a choice:
            ───────────────
            1) Display Sections with Courses.
            2) Display Courses.
            3) Assignment.
            Any other key to exit.
            ───> 
            """);

            choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Display.SectionCourse();
                    break;
                case "2":
                    Display.Course();
                    break;
                case "3":
                    Display.CourseInDetails();
                    break;
                default:
                    flag = false;
                    break;
            }

            Console.WriteLine("──────────────────────────────────────────────────────────────────\n");
        }
    }
}