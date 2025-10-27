using Shared;
using Microsoft.Data.SqlClient;
using Dapper_ORM.Classes;
using System.Data;
using Dapper;

namespace Dapper_ORM;

public abstract class Select
{
    public static void RawSql()
    {
        Utils.printTitle(title: "Select ( RawSql )", color: ConsoleColor.Blue, width: 70);

        string sql = "SELECT * FROM WALLETS";

        IEnumerable<dynamic> resultAsDynamic = Connection.db.Query(sql);
        resultAsDynamic.Print("Using Dynamic Query");

        IEnumerable<Wallet> resultAsWallet = Connection.db.Query<Wallet>(sql);
        resultAsWallet.Print("Using Typed Query");
    }

}