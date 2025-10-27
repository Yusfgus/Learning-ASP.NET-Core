using Shared;
using Microsoft.Data.SqlClient;
using Dapper_ORM.Classes;
using System.Data;
using Dapper;

namespace Dapper_ORM;

public abstract class Delete
{
    public static void RawSql()
    {
        Utils.printTitle(title: "Delete ( RawSql )", color: ConsoleColor.Blue, width: 70);

        Console.Write("Enter wallet id: ");
        int id = Convert.ToInt32(Console.ReadLine());

        var sql = "DELETE from Wallets " +
                "WHERE Id = @Id";

        var parameters = new { Id = id };
        int rowsAffected = Connection.db.Execute(sql, parameters);

        if (rowsAffected > 0)
            Console.WriteLine($"wallet deleted successfully");
        else
            Console.WriteLine($"ERROR: wallet was not deleted");
    }

}