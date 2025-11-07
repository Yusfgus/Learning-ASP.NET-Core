using System;
using System.Collections.Generic;
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

public abstract class ChangeTracking
{
    public static void Run()
    {
        Utils.printTitle(title: "Change Tracking", color: ConsoleColor.Blue, width: 70);

        string choice;
        bool flag = true;
        while (flag)
        {
            Console.Write("""
            Enter a choice:
            ───────────────
            1) As Tracking Entity.
            2) As UnTracking Entity.
            3) Insert Principal Entity.
            4) Insert Principal With Dependent Entity.
            5) Attach Existing Principal Entity.
            6) Update Principle Entity.
            7) Delete Principle Entity.
            8) Delete Dependent Entity.
            ───> 
            """);

            choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AsTrackedEntity();
                    break;
                case "2":
                    AsUnTrackedEntity();
                    break;
                case "3":
                    InsertPrincipalEntity();
                    break;
                case "4":
                    InsertPrincipalWithDependentEntity();
                    break;
                case "5":
                    AttachExistingPrincipalEntity();
                    break;
                case "6":
                    UpdatePrincipalEntity();
                    break;
                case "7":
                    DeletePrincipalEntity();
                    break;
                case "8":
                    DeleteDependentEntity();
                    break;
                default:
                    flag = false;
                    break;
            }

            Console.WriteLine("─────────────────────────────────\n");
        }
    }

    private static void AsTrackedEntity()
    {
        Utils.printTitle("As Tracked Entity");

        DatabaseHelper.RecreateCleanDatabase();
        DatabaseHelper.PopulateDatabase();

        using (var context = new AppDbContext())
        {
            var author = context.Authors.First(); // DB Query using LINQ

            Console.WriteLine("Initial");
            Console.WriteLine(context.ChangeTracker.DebugView.LongView); // Unchanged

            author.FName = "Eric";
            Console.WriteLine("After Changing FName");
            Console.WriteLine(context.ChangeTracker.DebugView.LongView); // Unchanged

            context.Entry(author).State = EntityState.Modified;
            Console.WriteLine("After Changing State to modified, before save changes");
            Console.WriteLine(context.ChangeTracker.DebugView.LongView); // Modified

            context.SaveChanges();
            Console.WriteLine("After Save Changes");
            Console.WriteLine(context.ChangeTracker.DebugView.LongView); // Unchanged
        }
    }

    private static void AsUnTrackedEntity()
    {
        Utils.printTitle("As UnTracked Entity");

        DatabaseHelper.RecreateCleanDatabase();
        DatabaseHelper.PopulateDatabase();

        using (var context = new AppDbContext())
        {
            var author = context.Authors.AsNoTracking().First();
            Console.WriteLine(context.ChangeTracker.DebugView.LongView);  // nothing
        }
    }

    private static void InsertPrincipalEntity()
    {
        Utils.printTitle("Insert Principal Entity");

        DatabaseHelper.RecreateCleanDatabase();

        using (var context = new AppDbContext())
        {
            // Mark book  as added
            context.Add(new Author { Id = 1, FName = "Eric", LName = "Evans" });

            Console.WriteLine("Before SaveChanges:");
            Console.WriteLine(context.ChangeTracker.DebugView.LongView); // Added

            context.SaveChanges();

            Console.WriteLine("After SaveChanges:");
            Console.WriteLine(context.ChangeTracker.DebugView.LongView); // Unchanged
        }
    }

    private static void InsertPrincipalWithDependentEntity()
    {
        Utils.printTitle("Insert Principal With Dependent Entity");

        DatabaseHelper.RecreateCleanDatabase();

        using (var context = new AppDbContext())
        {
            context.Add(new Author
            {
                Id = 1,
                FName = "Eric",
                LName = "Evans",
                Books = new List<Book>
                {
                    new Book
                    {
                        Id = 1,
                        Title = "Domain-Driven Design: Tackling Complexity in the Heart of Software"
                    },
                    new Book
                    {
                        Id = 2,
                        Title = "Domain-Driven Design Reference: Definitions and Pattern Summaries"
                    }
                }
            });

            Console.WriteLine("Before SaveChanges:");
            Console.WriteLine(context.ChangeTracker.DebugView.LongView); // Added, Added, Added

            context.SaveChanges();

            Console.WriteLine("After SaveChanges:");
            Console.WriteLine(context.ChangeTracker.DebugView.LongView); // Unchanged, Unchanged, Unchanged
        }
    }

    private static void AttachExistingPrincipalEntity()
    {
        Utils.printTitle("Attach Existing Principal Entity");

        DatabaseHelper.RecreateCleanDatabase();
        DatabaseHelper.PopulateDatabase();

        using (var context = new AppDbContext())
        {
            var author = new Author { Id = 1, FName = "Eric", LName = "Evans" }; // already in Database

            Console.WriteLine("Before Attach:");
            Console.WriteLine(context.ChangeTracker.DebugView.LongView); // nothing

            context.Attach(author); // start tracking

            author.LName = "Evanzzz";

            Console.WriteLine("Before SaveChanges:");
            Console.WriteLine(context.ChangeTracker.DebugView.LongView); // Unchanged

            context.SaveChanges();

            Console.WriteLine("After SaveChanges:");
            Console.WriteLine(context.ChangeTracker.DebugView.LongView); // Unchanged
        }
    }

    private static void UpdatePrincipalEntity()
    {
        Utils.printTitle("Update  Principle Entity");

        DatabaseHelper.RecreateCleanDatabase();
        DatabaseHelper.PopulateDatabase();

        using (var context = new AppDbContext())
        {
            // Mark book as modified
            context.Update(new Author { Id = 1, FName = "EricAAAAA", LName = "Evans" });

            Console.WriteLine("Before SaveChanges:");
            Console.WriteLine(context.ChangeTracker.DebugView.LongView); // Modified

            context.SaveChanges();

            Console.WriteLine("After SaveChanges:");
            Console.WriteLine(context.ChangeTracker.DebugView.LongView); // Unchanged
        }
    }

    private static void DeletePrincipalEntity()
    {
        Utils.printTitle("Delete  Principle Entity");

        DatabaseHelper.RecreateCleanDatabase();
        DatabaseHelper.PopulateDatabase();

        using (var context = new AppDbContext())
        {
            // Mark author as Deleted
            context.Remove(new Author { Id = 1 });

            Console.WriteLine("Before SaveChanges:");
            Console.WriteLine(context.ChangeTracker.DebugView.LongView); // Deleted

            context.SaveChanges();

            Console.WriteLine("After SaveChanges:");
            Console.WriteLine(context.ChangeTracker.DebugView.LongView); // nothing
        }
    }

    private static void DeleteDependentEntity()
    {
        Utils.printTitle("Delete Dependent Entity");

        DatabaseHelper.RecreateCleanDatabase();
        DatabaseHelper.PopulateDatabase();
        Console.ReadKey();

        using (var context = new AppDbContext())
        {
            var book = DatabaseHelper.GetDisconnectedBook()!; // from another context

            context.Attach(book); // attach to current context to be tracked

            // Mark book  as Deleted
            context.Remove(book);

            Console.WriteLine("Before SaveChanges:");
            Console.WriteLine(context.ChangeTracker.DebugView.LongView); // Deleted

            context.SaveChanges();

            Console.WriteLine("After SaveChanges:");
            Console.WriteLine(context.ChangeTracker.DebugView.LongView); // nothing
        }
    }
}