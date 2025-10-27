using Shared;
using Microsoft.Data.SqlClient;
using Dapper_ORM.Classes;
using System.Data;
using Dapper;

namespace Dapper_ORM;

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
            Console.WriteLine($"ERROR: wallet {{ {walletToUpdate} }} was not updated");
    }

}