using Shared;
using Microsoft.Data.SqlClient;
using ADO.NET.Classes;
using System.Data;

namespace ADO.NET;

public abstract class Update
{
    public static void RawSql()
    {
        Utils.printTitle(title: "Update ( RawSql )", color: ConsoleColor.Blue, width: 70);

        Wallet wallet = new Wallet();

        Console.Write("Enter wallet id: ");
        wallet.Id = Convert.ToInt32(Console.ReadLine());
        Console.Write("Enter new holder: ");
        wallet.Holder = Console.ReadLine();
        Console.Write("Enter new balance: ");
        wallet.Balance = Convert.ToInt32(Console.ReadLine());

        var sql = "UPDATE Wallets SET Holder = @Holder, Balance = @Balance " +
                $"WHERE Id = @Id";

        SqlParameter idParameter = new SqlParameter
        {
            ParameterName = "@Id",
            SqlDbType = SqlDbType.Int,
            Direction = ParameterDirection.Input,
            Value = wallet.Id,
        };
        SqlParameter holderParameter = new SqlParameter
        {
            ParameterName = "@Holder",
            SqlDbType = SqlDbType.VarChar,
            Direction = ParameterDirection.Input,
            Value = wallet.Holder,
        };

        SqlParameter balanceParameter = new SqlParameter
        {
            ParameterName = "@Balance",
            SqlDbType = SqlDbType.Decimal,
            Direction = ParameterDirection.Input,
            Value = wallet.Balance,
        };


        SqlCommand command = new SqlCommand(sql, Connection.conn);

        command.Parameters.Add(idParameter);
        command.Parameters.Add(holderParameter);
        command.Parameters.Add(balanceParameter);

        command.CommandType = CommandType.Text;

        Connection.conn.Open();

        if (command.ExecuteNonQuery() > 0)
        {
            Console.WriteLine($"wallet updated successfully");
        }


        Connection.conn.Close();
    }

}