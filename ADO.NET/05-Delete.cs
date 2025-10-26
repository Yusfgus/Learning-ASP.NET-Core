using Shared;
using Microsoft.Data.SqlClient;
using ADO.NET.Classes;
using System.Data;

namespace ADO.NET;

public abstract class Delete
{
    public static void RawSql()
    {
        Utils.printTitle(title: "Delete ( RawSql )", color: ConsoleColor.Blue, width: 70);

        Console.Write("Enter wallet id: ");
        int id = Convert.ToInt32(Console.ReadLine());

        var sql = "DELETE from Wallets " +
                "WHERE Id = @Id";

        SqlParameter idParameter = new SqlParameter
        {
            ParameterName = "@Id",
            SqlDbType = SqlDbType.Int,
            Direction = ParameterDirection.Input,
            Value = id,
        };


        SqlCommand command = new SqlCommand(sql, Connection.conn);

        command.Parameters.Add(idParameter);

        command.CommandType = CommandType.Text;

        Connection.conn.Open();

        if (command.ExecuteNonQuery() > 0)
        {
            Console.WriteLine($"wallet deleted successfully");
        }


        Connection.conn.Close();
    }


}