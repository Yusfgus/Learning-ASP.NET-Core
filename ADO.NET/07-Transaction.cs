using Shared;
using Microsoft.Data.SqlClient;
using System.Data;

namespace ADO.NET;

class Transaction
{
    public static void Update()
    {
        Utils.printTitle(title: "Transaction ( Update )", color: ConsoleColor.Blue, width: 70);

        SqlCommand command = Connection.conn.CreateCommand();

        command.CommandType = CommandType.Text;

        Connection.conn.Open();

        SqlTransaction transaction = Connection.conn.BeginTransaction();

        command.Transaction = transaction;

        try
        {
            command.CommandText = "UPDATE Wallets Set Balance = Balance - 1000 Where Id = 2";
            command.ExecuteNonQuery();

            command.CommandText = "UPDATE Wallets Set Balance = Balance + 1000 Where Id = 3";
            command.ExecuteNonQuery();

            transaction.Commit();

            Console.WriteLine("Transaction of transfer completed successfully");
        }
        catch
        {
            try
            {
                transaction.Rollback();
            }
            catch
            {
                // log errors
            }
        }
        finally
        {

            try
            {
                Connection.conn.Close();
            }
            catch
            {
                // log errors
            }
        }
    }
}