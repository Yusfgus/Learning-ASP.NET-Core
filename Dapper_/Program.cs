using Shared;

namespace Dapper_;

class Program
{
    static void Main()
    {
        Utils.printTitle(title: "Hello World! ( Dapper )", color: ConsoleColor.Red, width: 80);
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
            4) Update Using RawSql.
            5) Delete Using RawSql.
            6) Execute Multiple Query.
            7) Transaction of Update.
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
                    Update.RawSql();
                    break;
                case "5":
                    Delete.RawSql();
                    break;
                case "6":
                    MultipleQuery.RawSql();
                    break;
                case "7":
                    Transaction.RawSql();
                    break;
                default:
                    flag = false;
                    break;
            }

            Console.WriteLine("──────────────────────────────────────────────────────────────────\n");
        }


        // Console.ReadKey();
    }
}