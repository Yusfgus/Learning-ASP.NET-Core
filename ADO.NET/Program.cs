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
            2) Insert Using RawSql.
            3) Insert Using ExecuteScalar.
            4) Insert Using Stored Procedure.
            5) Update Using RawSql.
            6) Delete Using RawSql.
            7) Select Using DataAdapter.
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
                    Insert.RawSql();
                    break;
                case "3":
                    Insert.ExecuteScalar();
                    break;
                case "4":
                    Insert.StoredProcedure();
                    break;
                case "5":
                    Update.RawSql();
                    break;
                case "6":
                    Delete.RawSql();
                    break;
                case "7":
                    DataAdapter.Select();
                    break;
                case "8":
                    Transaction.Update();
                    break;
                default:
                    flag = false;
                    break;
            }
        }


        // Console.ReadKey();
    }
}