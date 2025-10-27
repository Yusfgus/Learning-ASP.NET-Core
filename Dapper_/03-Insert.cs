using Shared;
using Microsoft.Data.SqlClient;
using Dapper_.Classes;
using System.Data;
using Dapper;

namespace Dapper_;

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

        // var parameters = new { Holder = walletToInsert.Holder, Balance = walletToInsert.Balance };
        // int rowsAffected = Connection.db.Execute(sql, parameters);
        
        int rowsAffected = Connection.db.Execute(sql, walletToInsert);  // Note: params names must be the same as fields

        if (rowsAffected > 0)
            Console.WriteLine($"wallet {{ {walletToInsert} }} added successfully");
        else
            Console.WriteLine($"ERROR: wallet {{ {walletToInsert} }} was not added");
    }

    public static void ExecuteScalar()
    {
        Utils.printTitle(title: "Insert ( ExecuteScalar )", color: ConsoleColor.Blue, width: 70);

        var walletToInsert = new Wallet();

        Console.Write("Enter wallet holder: ");
        walletToInsert.Holder = Console.ReadLine();

        Console.Write("Enter wallet balance: ");
        walletToInsert.Balance = Convert.ToInt32(Console.ReadLine());


        var sql = "INSERT INTO WALLETS (Holder, Balance) " +
                "VALUES (@Holder, @Balance) " + 
                "Select Cast(scope_identity() As int)";

        // var parameters = new { Holder = walletToInsert.Holder, Balance = walletToInsert.Balance };
        // walletToInsert.Id = Connection.db.Query<int>(sql, walletToInsert).Single();
        
        walletToInsert.Id = Connection.db.Query<int>(sql, walletToInsert).Single();  // Note: params names must be the same as fields

        Console.WriteLine($"wallet {{ {walletToInsert} }} added successfully");
    }

}