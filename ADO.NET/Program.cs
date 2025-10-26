using Shared;

namespace ADO.NET;

class Program
{
    static void Main()
    {
        Utils.printTitle(title: "Hello World! ( ADO.NET )", color: ConsoleColor.Red, width: 80);

        RawSql.ConnectionString();

        RawSql.Select();

        // Console.ReadKey();
    }
}