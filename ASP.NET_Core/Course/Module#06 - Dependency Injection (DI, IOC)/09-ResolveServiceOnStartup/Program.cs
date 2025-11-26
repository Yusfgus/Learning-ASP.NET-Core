
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<DbInitializer>();

var app = builder.Build();

// Runs ones at startup
// Execute a behavior once without a real HTTP request
using (IServiceScope scope = app.Services.CreateScope())
{
    IServiceProvider sp = scope.ServiceProvider;

    DbInitializer initializer = sp.GetRequiredService<DbInitializer>();

    initializer.Initialize();
}
// You must use this for scoped services, because the root container doesn't know hot to behave like a request


app.Run();

public class DbInitializer
{
    private readonly ILogger _logger;
    public DbInitializer(ILogger<DbInitializer> logger)
    {
        _logger = logger;
    }

    public void Initialize()
    {
        _logger.LogInformation("1000 item were seeded successfully");
    }
}
