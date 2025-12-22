
using LoggingConfigurationInternal.Services;
using Microsoft.Extensions.Logging.Console;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.SetMinimumLevel(LogLevel.Error); // global
builder.Logging.AddFilter("Microsoft", LogLevel.Error);
builder.Logging.AddFilter("Microsoft.Hosting.Lifetime", LogLevel.Information);

builder.Logging.AddFilter<ConsoleLoggerProvider>((string? category, LogLevel level) =>
{
    if (category != null && category.StartsWith("Microsoft"))
        return level >= LogLevel.Information;

    if (category != null && category.StartsWith("LoggingConfigurationInternal.Services"))
        return level >= LogLevel.Trace;

    return level >= LogLevel.Error;
});

builder.Services.AddControllers();

builder.Services.AddScoped<OrderService>();

var app = builder.Build();

app.MapControllers();

app.Run();
