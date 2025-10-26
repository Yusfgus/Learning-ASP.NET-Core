using Shared;
using Microsoft.Data.SqlClient;
using ADO.NET.Classes;
using System.Data;

namespace ADO.NET;

public abstract class Insert
{
    public static void RawSql()
    {
        Utils.printTitle(title: "Insert ( RawSql )", color: ConsoleColor.Blue, width: 70);

        var walletToInsert = new Wallet();

        Console.Write("Enter wallet holder: ");
        walletToInsert.Holder = Console.ReadLine();

        Console.Write("Enter wallet balance: ");
        walletToInsert.Balance = Convert.ToInt32(Console.ReadLine());


        var sql = "INSERT INTO WALLETS (Holder, Balance) VALUES " +
            $"(@Holder, @Balance)";

        SqlParameter holderParameter = new SqlParameter
        {
            ParameterName = "@Holder",
            SqlDbType = SqlDbType.VarChar,
            Direction = ParameterDirection.Input,
            Value = walletToInsert.Holder,
        };

        SqlParameter balanceParameter = new SqlParameter
        {
            ParameterName = "@Balance",
            SqlDbType = SqlDbType.Decimal,
            Direction = ParameterDirection.Input,
            Value = walletToInsert.Balance,
        };


        SqlCommand command = new SqlCommand(sql, Connection.conn);

        command.Parameters.Add(holderParameter);
        command.Parameters.Add(balanceParameter);

        command.CommandType = CommandType.Text;

        Connection.conn.Open();

        int rowsAffected = command.ExecuteNonQuery();

        if (rowsAffected > 0)
            Console.WriteLine($"wallet for {{ {walletToInsert.Holder} }} added successfully");
        else
            Console.WriteLine($"ERROR: wallet {{ {walletToInsert} }} was not added");

        Connection.conn.Close();
    }

    public static void ExecuteScalar()
    {
        Utils.printTitle(title: "Insert ( ExecuteScalar )", color: ConsoleColor.Blue, width: 70);

        var walletToInsert = new Wallet();

        Console.Write("Enter wallet holder: ");
        walletToInsert.Holder = Console.ReadLine();

        Console.Write("Enter wallet balance: ");
        walletToInsert.Balance = Convert.ToInt32(Console.ReadLine());

        var sql = "INSERT INTO WALLETS (Holder, Balance) VALUES " +
                $"(@Holder, @Balance);" +
                $"SELECT Cast(scope_identity() AS int)";

        SqlParameter holderParameter = new SqlParameter
        {
            ParameterName = "@Holder",
            SqlDbType = SqlDbType.VarChar,
            Direction = ParameterDirection.Input,
            Value = walletToInsert.Holder,
        };

        SqlParameter balanceParameter = new SqlParameter
        {
            ParameterName = "@Balance",
            SqlDbType = SqlDbType.Decimal,
            Direction = ParameterDirection.Input,
            Value = walletToInsert.Balance,
        };


        SqlCommand command = new SqlCommand(sql, Connection.conn);

        command.Parameters.Add(holderParameter);
        command.Parameters.Add(balanceParameter);

        command.CommandType = CommandType.Text;

        Connection.conn.Open();

        walletToInsert.Id = (int)command.ExecuteScalar();

        Console.WriteLine($"wallet id = {walletToInsert.Id}");

        Console.WriteLine($"wallet {{ {walletToInsert} }} added successfully");

        Connection.conn.Close();
    }

    public static void StoredProcedure()
    {
        Utils.printTitle(title: "Insert ( Stored Procedure )", color: ConsoleColor.Blue, width: 70);

        var walletToInsert = new Wallet();

        Console.Write("Enter wallet holder: ");
        walletToInsert.Holder = Console.ReadLine();

        Console.Write("Enter wallet balance: ");
        walletToInsert.Balance = Convert.ToInt32(Console.ReadLine());

        SqlParameter holderParameter = new SqlParameter
        {
            ParameterName = "@Holder",
            SqlDbType = SqlDbType.VarChar,
            Direction = ParameterDirection.Input,
            Value = walletToInsert.Holder,
        };

        SqlParameter balanceParameter = new SqlParameter
        {
            ParameterName = "@Balance",
            SqlDbType = SqlDbType.Decimal,
            Direction = ParameterDirection.Input,
            Value = walletToInsert.Balance,
        };

        SqlCommand command = new SqlCommand("AddWallet", Connection.conn);

        command.Parameters.Add(holderParameter);
        command.Parameters.Add(balanceParameter);

        command.CommandType = CommandType.StoredProcedure;

        Connection.conn.Open();

        if (command.ExecuteNonQuery() > 0)
        {
            Console.WriteLine($"wallet for {walletToInsert.Holder} added successfully");
        }
        else
        {
            Console.WriteLine($"ERROR: wallet for {walletToInsert.Holder} was not added");
        }

        Connection.conn.Close();
    }

}