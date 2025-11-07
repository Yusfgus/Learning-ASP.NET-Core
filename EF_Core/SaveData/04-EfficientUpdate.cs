using System;
using System.Linq;
using C01.BasicSaveWithTracking.Helpers;
using EF_Core.SaveData.Data;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace EF_Core.SaveData;

public abstract class EfficientUpdate
{
    public static void Run()
    {
        Utils.printTitle(title: "Efficient Update", color: ConsoleColor.Blue, width: 70);

        string choice;
        bool flag = true;
        while (flag)
        {
            Console.Write("""
            Enter a choice:
            ───────────────
            1) Update Many ( Typical Implementation ).
            2) Update Many ( EF7+ Implementation ).
            3) Update Many ( EF7+ Raw Sql ).
            4) Delete Many ( EF7+ Implementation ).
            ───> 
            """);

            choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    UpdateMany_Typical_Implementation();
                    break;
                case "2":
                    UpdateMany_EF7_Implementation();
                    break;
                case "3":
                    UpdateMany_EF7_RawSql();
                    break;
                case "4":
                    DeleteMany_EF7_Implementation();
                    break;
                default:
                    flag = false;
                    break;
            }

            Console.WriteLine("─────────────────────────────────\n");
        }
    }


    private static void UpdateMany_Typical_Implementation()
    {
        Utils.printTitle("Update Many ( Typical Implementation )");

        DatabaseHelper.RecreateCleanDatabase();
        DatabaseHelper.PopulateDatabase();
        Console.WriteLine("Check database then press any key to continue... "); Console.ReadKey();

        using (var context = new AppDbContext())
        {
            var author1Books = context.Books.Where(x => x.AuthorId == 1);

            foreach (var book in author1Books) // Deferred Execution
            {
                book.Price *= 1.1m;
            }

            // Many small update queries

            context.SaveChanges();
        }
    }

    private static void UpdateMany_EF7_Implementation()
    {
        Utils.printTitle("Update Many ( EF7+ Implementation )");

        DatabaseHelper.RecreateCleanDatabase();
        DatabaseHelper.PopulateDatabase();
        Console.WriteLine("Check database then press any key to continue... "); Console.ReadKey();

        using (var context = new AppDbContext())
        {
            context.Books.Where(x => x.AuthorId == 1)
                    .ExecuteUpdate(b => b.SetProperty(p => p.Price, p => p.Price * 1.1m));

            // Single big update query

            // context.SaveChanges() // No need to 
        }
    }

    private static void UpdateMany_EF7_RawSql()
    {
        Utils.printTitle("Update Many ( EF7+ Raw Sql )");

        DatabaseHelper.RecreateCleanDatabase();
        DatabaseHelper.PopulateDatabase();
        Console.WriteLine("Check database then press any key to continue... "); Console.ReadKey();

        using (var context = new AppDbContext())
        {
            context.Database.ExecuteSql($"UPDATE dbo.Books SET Price = Price * 1.1 WHERE AuthorId = 1");

            // Single big update query

            // context.SaveChanges() // No need to  
        }
    }

    private static void DeleteMany_EF7_Implementation()
    {
        Utils.printTitle("Delete Many ( EF7+ Implementation )");

        DatabaseHelper.RecreateCleanDatabase();
        DatabaseHelper.PopulateDatabase();
        Console.WriteLine("Check database then press any key to continue... "); Console.ReadKey();

        using (var context = new AppDbContext())
        {
            context.Books.Where(x => x.Title.StartsWith("Book"))
                        .ExecuteDelete();

            // context.SaveChanges() // No need to  
        }
    }

}