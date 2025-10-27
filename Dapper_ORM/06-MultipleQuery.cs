using Shared;
using Microsoft.Data.SqlClient;
using Dapper_ORM.Classes;
using System.Data;
using Dapper;

namespace Dapper_ORM;

public abstract class MultipleQuery
{
    public static void RawSql()
    {
        Utils.printTitle(title: "Execute Multiple Query", color: ConsoleColor.Blue, width: 70);

        string query = "Select MIN(Balance) From Wallets;" +
                        "Select MAX(Balance) From Wallets";

        var multi = Connection.db.QueryMultiple(query);

        Console.WriteLine($"Min = {multi.ReadSingle<decimal>()}" +
                        $"\nMax = {multi.ReadSingle<decimal>()}");

        // Console.WriteLine($"Min = {multi.Read<decimal>().Single()}" +
        //                 $"\nMax = {multi.Read<decimal>().Single()}");
    }

}