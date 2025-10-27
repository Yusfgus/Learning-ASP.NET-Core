using Shared;
using Microsoft.Data.SqlClient;
using Dapper_ORM.Classes;
using System.Data;
using Dapper;
using System.Transactions;

namespace Dapper_ORM;

public abstract class Transaction
{
    public static void RawSql()
    {
        Utils.printTitle(title: "Transaction", color: ConsoleColor.Blue, width: 70);

        Console.Write("Enter from wallet id: ");
        int fromId = Convert.ToInt32(Console.ReadLine());
        Console.Write("Enter to wallet id: ");
        int toId = Convert.ToInt32(Console.ReadLine());
        Console.Write("Enter amount to transfer: ");
        decimal amount = Convert.ToDecimal(Console.ReadLine());

        using (TransactionScope transactionScope = new TransactionScope())
        { 
            int rowsAffected1 = Connection.db.Execute("UPDATE Wallets Set Balance = Balance - @Amount Where Id = @Id",
                                                        new { Amount = amount, Id = fromId });

            int rowsAffected2 = Connection.db.Execute("UPDATE Wallets Set Balance = Balance + @Amount Where Id = @Id",
                                                    new { Amount = amount, Id = toId });

            if (rowsAffected1 > 0 && rowsAffected2 > 0)
            {
                Console.WriteLine("Transaction of transfer completed successfully");
            }
            else
            {
                Console.WriteLine($"ERROR: Transaction of transfer failed");
            }

            transactionScope.Complete();
        }
    }

}