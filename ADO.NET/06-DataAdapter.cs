using Shared;
using Microsoft.Data.SqlClient;
using ADO.NET.Classes;
using System.Data;

namespace ADO.NET;

class DataAdapter
{
    public static void Select()
    {
        Utils.printTitle(title: "Data Adapter ( Select )", color: ConsoleColor.Blue, width: 70);

        var sql = "SELECT * FROM WALLETS";

        Connection.conn.Open();

        SqlDataAdapter adapter = new SqlDataAdapter(sql, Connection.conn);

        DataTable dt = new DataTable();

        adapter.Fill(dt);

        Connection.conn.Close();

        foreach (DataRow dr in dt.Rows)
        {
            var wallet = new Wallet
            {
                Id = Convert.ToInt32(dr["Id"]),
                Holder = Convert.ToString(dr["Holder"]),
                Balance = Convert.ToDecimal(dr["Balance"]),
            };

            Console.WriteLine(wallet);
        }
    }

}