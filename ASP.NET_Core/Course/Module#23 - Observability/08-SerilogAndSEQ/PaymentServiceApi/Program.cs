using System.Text.Json.Serialization;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// // add Serilog provider with the other default providers
// builder.Services.AddSerilog();

// add Serilog provider only
builder.Host.UseSerilog((context, loggerConfig) =>
{
    loggerConfig.ReadFrom.Configuration(builder.Configuration);
});

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

var app = builder.Build();

app.UseSerilogRequestLogging();

app.MapControllers();

app.Run();
