using Shared;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using ADO.NET.Classes;
using System.Data;
using System.Data.Common;

namespace ADO.NET;

public class RawSql
{
    private static SqlConnection conn;

    public static void ConnectionString()
    {
        var configuration = new ConfigurationBuilder()
                            .AddJsonFile("appsettings.json")
                            .Build();

        string conStr = configuration.GetSection("constr").Value;
        Console.WriteLine(conStr);

        conn = new SqlConnection(conStr);
    }

    public static void Select()
    {
        Utils.printTitle(title: "RawSal ( Select )", color: ConsoleColor.Blue, width: 70);

        var sql = "SELECT * FROM WALLETS";

        SqlCommand command = new SqlCommand(sql, conn);

        command.CommandType = CommandType.Text;

        conn.Open();

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

        conn.Close();
    }
}