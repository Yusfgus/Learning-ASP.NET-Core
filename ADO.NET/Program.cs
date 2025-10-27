using System.Data;
using Shared;

namespace ADO.NET;

class Program
{
    static void Main()
    {
        Utils.printTitle(title: "Hello World! ( ADO.NET )", color: ConsoleColor.Red, width: 80);

        Connection.SetConnection();

        string? choice;
        bool flag = true;
        while (flag)
        {
            Console.Write("""
            
            Enter a choice:
            1) Select Using RawSql.
            2) Select Using DataAdapter.
            3) Insert Using RawSql.
            4) Insert Using ExecuteScalar.
            5) Insert Using Stored Procedure.
            6) Update Using RawSql.
            7) Delete Using RawSql.
            8) Update Using Transaction.
            Any other key to exit.
            ---> 
            """);

            choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Select.RawSql();
                    break;
                case "2":
                    DataAdapter.Select();
                    break;
                case "3":
                    Insert.RawSql();
                    break;
                case "4":
                    Insert.ExecuteScalar();
                    break;
                case "5":
                    Insert.StoredProcedure();
                    break;
                case "6":
                    Update.RawSql();
                    break;
                case "7":
                    Delete.RawSql();
                    break;
                case "8":
                    Transaction.Update();
                    break;
                default:
                    flag = false;
                    break;
            }

            Console.WriteLine("──────────────────────────────────────────────────────────────────");
        }


        // Console.ReadKey();
    }
}