using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Dapper_;

public abstract class Connection
{
    public static IDbConnection db = new SqlConnection();

    public static void SetConnection()
    {
        var configuration = new ConfigurationBuilder()
                            .AddJsonFile("appsettings.json")
                            .Build();

        string? conStr = configuration.GetSection("constr").Value;
        Console.WriteLine(conStr);

        db = new SqlConnection(conStr);
    }

}