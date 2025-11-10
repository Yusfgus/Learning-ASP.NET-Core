using System;
using System.Linq;
using EF_Core.Interceptors.Data;
using EF_Core.Interceptors.Helpers;
using Shared;

namespace EF_Core.Interceptors;

class Program
{
    static void Main()
    {
        Utils.printTitle(title: "EF_Core ( Interceptors )", color: ConsoleColor.Red, width: 80);

        DatabaseHelper.RecreateCleanDatabase();
        DatabaseHelper.PopulateDatabase();

        Console.WriteLine();
        Console.WriteLine("Before Delete");

        DatabaseHelper.ShowBooks();

        using (var context = new AppDbContext())
        {
            var book = context.Books.First();
            context.Books.Remove(book);
            context.SaveChanges();
        }
        Console.WriteLine();
        Console.WriteLine("After Delete Book Id = '1'");

        DatabaseHelper.ShowBooks();
    }
}