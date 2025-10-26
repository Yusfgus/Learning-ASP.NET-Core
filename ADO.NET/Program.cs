using System.Data;
using Shared;

namespace ADO.NET;

class Program
{
    static void Main()
    {
        Utils.printTitle(title: "Hello World! ( ADO.NET )", color: ConsoleColor.Red, width: 80);

        Connection.SetConnection();

        string? choice, ans = "y";
        while (ans == "y")
        {
            Console.Write("""
            
            Enter a choice:
            1- Select Using RawSql.
            2- Insert Using RawSql.
            3- Insert Using ExecuteScalar.
            4- Insert Using Stored Procedure.
            ---> 
            """);

            choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Select.RawSql();
                    break;
                case "2":
                    Insert.RawSql();
                    break;
                case "3":
                    Insert.ExecuteScalar();
                    break;
                case "4":
                    Insert.StoredProcedure();
                    break;
                default:
                    Console.WriteLine($"Invalid input ({choice})");
                    break;
            }

            Console.Write("\nDo you want to continue (y) ? ");
            ans = Console.ReadLine();
        }


        // Console.ReadKey();
    }
}