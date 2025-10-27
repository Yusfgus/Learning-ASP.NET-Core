using Shared;
using Microsoft.Data.SqlClient;
using Dapper_.Classes;
using System.Data;
using Dapper;

namespace Dapper_;

public abstract class Update
{
    public static void RawSql()
    {
        Utils.printTitle(title: "Update ( RawSql )", color: ConsoleColor.Blue, width: 70);

        Wallet walletToUpdate = new Wallet();

        Console.Write("Enter wallet id: ");
        walletToUpdate.Id = Convert.ToInt32(Console.ReadLine());
        Console.Write("Enter new holder: ");
        walletToUpdate.Holder = Console.ReadLine();
        Console.Write("Enter new balance: ");
        walletToUpdate.Balance = Convert.ToInt32(Console.ReadLine());

        var sql = "UPDATE Wallets SET Holder = @Holder, Balance = @Balance " +
                $"WHERE Id = @Id";

        // var parameters = new { Holder = walletToUpdate.Holder, Balance = walletToUpdate.Balance };
        // int rowsAffected = Connection.db.Execute(sql, parameters);

        int rowsAffected = Connection.db.Execute(sql, walletToUpdate);  // Note: params names must be the same as fields

        if (rowsAffected > 0)
            Console.WriteLine($"wallet {{ {walletToUpdate} }} updated successfully");
        else
            Console.WriteLine($"ERROR: wallet {{ {walletToUpdate} }} was not added");
    }

    public static void ExecuteScalar()
    {
        Utils.printTitle(title: "Insert ( ExecuteScalar )", color: ConsoleColor.Blue, width: 70);

        var walletToUpdate = new Wallet();

        Console.Write("Enter wallet holder: ");
        walletToUpdate.Holder = Console.ReadLine();

        Console.Write("Enter wallet balance: ");
        walletToUpdate.Balance = Convert.ToInt32(Console.ReadLine());


        var sql = "INSERT INTO WALLETS (Holder, Balance) " +
                "VALUES (@Holder, @Balance) " + 
                "Select Cast(scope_identity() As int)";

        // var parameters = new { Holder = walletToUpdate.Holder, Balance = walletToUpdate.Balance };
        // walletToUpdate.Id = Connection.db.Query<int>(sql, walletToUpdate).Single();
        
        walletToUpdate.Id = Connection.db.Query<int>(sql, walletToUpdate).Single();  // Note: params names must be the same as fields

        Console.WriteLine($"wallet {{ {walletToUpdate} }} added successfully");
    }

}