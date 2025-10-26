using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;

namespace ADO.NET;

public abstract class Connection
{
    public static SqlConnection conn = new SqlConnection();

    public static void SetConnection()
    {
        var configuration = new ConfigurationBuilder()
                            .AddJsonFile("appsettings.json")
                            .Build();

        string? conStr = configuration.GetSection("constr").Value;
        Console.WriteLine(conStr);

        conn = new SqlConnection(conStr);
    }

}