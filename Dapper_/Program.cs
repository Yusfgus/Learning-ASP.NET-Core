using Shared;

namespace Dapper_;

class Program
{
    static void Main()
    {
        Utils.printTitle(title: "Hello World! ( Dapper )", color: ConsoleColor.Red, width: 80);
        Connection.SetConnection();

        char choice;
        bool flag = true;
        while (flag)
        {
            Console.Write("""
            Enter a choice:
            1) Select Using RawSql.
            2) Insert Using RawSql.
            3) Insert Using ExecuteScalar.
            4) Update Using RawSql.
            Any other key to exit.
            ---> 
            """);

            choice = Console.ReadKey().KeyChar;
            Console.WriteLine();

            switch (choice)
            {
                case '1':
                    Select.RawSql();
                    break;
                case '2':
                    Insert.RawSql();
                    break;
                case '3':
                    Insert.ExecuteScalar();
                    break;
                case '4':
                    Update.RawSql();
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