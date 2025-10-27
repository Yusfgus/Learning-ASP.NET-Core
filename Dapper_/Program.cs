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
                default:
                    flag = false;
                    break;
            }

            Console.WriteLine("──────────────────────────────────────────────────────────────────\n");
        }


        // Console.ReadKey();
    }
}