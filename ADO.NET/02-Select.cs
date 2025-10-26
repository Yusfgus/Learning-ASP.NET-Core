using Shared;
using Microsoft.Data.SqlClient;
using ADO.NET.Classes;
using System.Data;

namespace ADO.NET;

public abstract class Select
{
    public static void RawSql()
    {
        Utils.printTitle(title: "Select ( RawSql )", color: ConsoleColor.Blue, width: 70);

        var sql = "SELECT * FROM WALLETS";

        SqlCommand command = new SqlCommand(sql, Connection.conn);

        command.CommandType = CommandType.Text;

        Connection.conn.Open();

        SqlDataReader reader = command.ExecuteReader();

        Wallet wallet;

        while (reader.Read())
        {
            wallet = new Wallet
            {
                Id = reader.GetInt32("Id"),
                Holder = reader.GetString("Holder"),
                Balance = reader.GetDecimal("Balance"),
            };

            Console.WriteLine(wallet);
        }

        Connection.conn.Close();
    }

}