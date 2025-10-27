using Microsoft.Extensions.Configuration;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Mapping.ByCode;

using NHibernate_ORM.Classes;
namespace NHibernate_ORM;

public abstract class Session
{
    private static string? conStr = String.Empty;

    public static void GetConnectionString()
    {
        if (conStr != String.Empty)
            return;
            
        var configuration = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json")
                    .Build();

        conStr = configuration.GetSection("constr").Value;
        Console.WriteLine(conStr);
    }

    public static ISession CreateSession(bool log = false)
    {
        ModelMapper mapper = new ModelMapper();

        // list all of type mappings from assembly
        mapper.AddMappings(typeof(Wallet).Assembly.ExportedTypes);

        // Compile class mapping
        HbmMapping domainMapping = mapper.CompileMappingForAllExplicitlyAddedEntities();

        // log xml (optional)
        // Console.WriteLine(domainMapping.AsString());

        // allow the application to specify properties and mapping documents to be used when creating
        Configuration hbConfig = new Configuration();

        // settings from app to NHibernate 
        hbConfig.DataBaseIntegration(c =>
        {
            // strategy to interact with provider
            c.Driver<MicrosoftDataSqlClientDriver>();

            // dialect NHibernate uses to build syntax to rdbms
            c.Dialect<MsSql2012Dialect>();

            // connection string
            c.ConnectionString = conStr;

            // log sql statement to console (optional)
            c.LogSqlInConsole = log;

            // format logged sql statement (optional)
            c.LogFormattedSql = log;
        });

        // add mapping to NHibernate configuration
        hbConfig.AddMapping(domainMapping);

        // instantiate a new ISessionFactory (use properties, settings and mapping)
        ISessionFactory sessionFactory = hbConfig.BuildSessionFactory();

        ISession session = sessionFactory.OpenSession();

        return session;
    }

}