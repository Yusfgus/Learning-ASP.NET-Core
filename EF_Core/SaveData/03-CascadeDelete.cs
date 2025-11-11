using System;
using System.Linq;
using C01.BasicSaveWithTracking.Helpers;
using EF_Core.SaveData.Data;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace EF_Core.SaveData;

public abstract class CascadeDelete
{
    public static void Run()
    {
        Utils.printTitle(title: "Cascade Delete", color: ConsoleColor.Blue, width: 70);

        string choice;
        bool flag = true;
        while (flag)
        {
            Console.Write("""
            Enter a choice:
            ───────────────
            1) Delete Principle with Dependent Entity ( FK Required ).
            2) Delete Principle with Dependent Entity ( FK Optional ).
            3) Sever Relationship From Principle Entity ( FK Optional ).
            4) Sever Relationship From Dependent Entity ( FK Optional ).
            ───> 
            """);

            choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    DeletePrincipalWithDependent_FK_Required();
                    break;
                case "2":
                    DeletePrincipalWithDependent_FK_Optional();
                    break;
                case "3":
                    SeverRelationshipFromPrinciple_FK_Optional();
                    break;
                case "4":
                    SeverRelationshipFromDependent_FK_Optional();
                    break;
                default:
                    flag = false;
                    break;
            }

            Console.WriteLine("─────────────────────────────────\n");
        }
    }

    public static void DeletePrincipalWithDependent_FK_Required()
    {
        Utils.printTitle("Delete Principle with Dependent Entity ( FK Required )");

        DatabaseHelper.RecreateCleanDatabase();
        DatabaseHelper.PopulateDatabase();
        Console.WriteLine("Check database then press any key to continue... "); Console.ReadKey();

        using (var context = new AppDbContext())
        {
            var author = context.Authors.First();

            context.Authors.Remove(author); // his books also removed

            context.SaveChanges();

            Console.WriteLine("Author deleted successfully");
        }
    }

    public static void DeletePrincipalWithDependent_FK_Optional()
    {
        Utils.printTitle("Delete Principle with Dependent Entity ( FK Optional )");

        DatabaseHelper.RecreateCleanDatabase();
        DatabaseHelper.PopulateDatabaseV2();
        Console.Write("Check database then press any key to continue... "); Console.ReadKey();

        using (var context = new AppDbContext())
        {
            // var author = context.AuthorV2s.Include(a => a.BookV2s).First(); // use if no OnDeleteBehavior in Configurations
            var author = context.AuthorV2s.First();

            context.AuthorV2s.Remove(author); // books not remove, but AuthorV2Id set to null

            context.SaveChanges();

            Console.WriteLine("Author deleted successfully");
        }
    }

    public static void SeverRelationshipFromPrinciple_FK_Optional()
    {
        Utils.printTitle("Sever Relationship From Principle Entity ( FK Optional )");

        DatabaseHelper.RecreateCleanDatabase();
        DatabaseHelper.PopulateDatabaseV2();
        Console.Write("Check database then press any key to continue... "); Console.ReadKey();

        using (var context = new AppDbContext())
        {
            // var author = context.AuthorV2s.First(); // won't be cleared bec. books are not tracked
            var author = context.AuthorV2s
                        .Include(x => x.BookV2s)
                        .First();

            author.BookV2s.Clear(); // AuthorV2Id set to null in books

            context.SaveChanges();

            Console.WriteLine("Relationship severed successfully");
        }
    }

    public static void SeverRelationshipFromDependent_FK_Optional()
    {
        Utils.printTitle("Sever Relationship From Dependent Entity ( FK Optional )");

        DatabaseHelper.RecreateCleanDatabase();
        DatabaseHelper.PopulateDatabaseV2();
        Console.Write("Check database then press any key to continue... "); Console.ReadKey();

        using (var context = new AppDbContext())
        {
            var author = context.AuthorV2s
                        .Include(x => x.BookV2s)
                        .First();

            foreach (var book in author.BookV2s)
            {
                book.AuthorV2 = null;
            }

            context.SaveChanges();

            Console.WriteLine("Relationship severed successfully");
        }
    }

}