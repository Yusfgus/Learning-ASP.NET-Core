using System;
using System.Linq;
using C01.BasicSaveWithTracking.Helpers;
using EF_Core.SaveData.Data;
using EF_Core.SaveData.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Shared;

namespace EF_Core.SaveData;

public abstract class BasicSave
{
    public static void Run()
    {
        Utils.printTitle(title: "Basic Save", color: ConsoleColor.Blue, width: 70);

        string choice;
        bool flag = true;
        while (flag)
        {
            Console.Write("""
            Enter a choice:
            ───────────────
            1) Basic Save.
            2) Basic Update.
            3) Basic Delete.
            4) Multiple Operations.
            5) Related Entities.
            ───> 
            """);

            choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Insert();
                    break;
                case "2":
                    Update();
                    break;
                case "3":
                    Delete();
                    break;
                case "4":
                    MultipleOperations();
                    break;
                case "5":
                    RelatedEntities();
                    break;
                default:
                    flag = false;
                    break;
            }

            Console.WriteLine("─────────────────────────────────\n");
        }
    }

    private static void Insert()
    {
        Utils.printTitle("Basic Insert");

        // DatabaseHelper.RecreateCleanDatabase();

        Author author = new Author { Id = 1, FName = "eric", LName = "Evans" };

        using (var context = new AppDbContext())
        {
            context.Authors.Add(author);

            context.SaveChanges();

            Console.WriteLine($"Author {author} inserted successfully");
        }
    }

    private static void Update()
    {
        Utils.printTitle(" Basic Update");

        using (var context = new AppDbContext())
        {
            Author author = context.Authors.FirstOrDefault();

            author.FName = "Eric";

            context.SaveChanges();

            Console.WriteLine($"Author {author} updated successfully");
        }
    }

    private static void Delete()
    {
        Utils.printTitle(" Basic Delete");

        using (var context = new AppDbContext())
        {
            Author author = context.Authors.FirstOrDefault();

            context.Authors.Remove(author);

            context.SaveChanges();

            Console.WriteLine($"Author {author} deleted successfully");
        }
    }

    private static void MultipleOperations()
    {
        Utils.printTitle(" Multiple Operations");

        using (var context = new AppDbContext())
        {
            var author1 = new Author { Id = 1, FName = "Eric", LName = "Evans" };
            context.Authors.Add(author1);

            var author2 = new Author { Id = 2, FName = "John", LName = "Skeet" };
            context.Authors.Add(author2);

            var author3 = new Author { Id = 3, FName = "Ahmad", LName = "Mohamed" };
            context.Authors.Add(author3);

            author3.FName = "Ahmed";

            context.SaveChanges();

            Console.WriteLine($"Authors inserted successfully");
        }
    }

    private static void RelatedEntities()
    {
        Utils.printTitle(" Multiple Operations");

        using (var context = new AppDbContext())
        {
            var author = context.Authors.FirstOrDefault();

            author.Books.Add(new Book
            {
                Id = 1,
                Title = "Domain-Driven Design: Tackling Complexity in the Heart of Software"
            });

            context.SaveChanges();

            Console.WriteLine("Book inserted and added to Author successfully");
        }
    }
    
}