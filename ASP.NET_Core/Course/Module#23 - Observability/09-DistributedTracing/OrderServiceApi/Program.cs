
using DistributedTracing.OrderServiceApi.Repositories;
using DistributedTracing.OrderServiceApi.Services;
using DistributedTracing.OrderServiceApi.Data;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Serilog;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

var builder = WebApplication.CreateBuilder(args);

// // add Serilog provider with the other default providers
// builder.Services.AddSerilog();

// add Serilog provider only
builder.Host.UseSerilog((context, loggerConfig) =>
{
    loggerConfig.ReadFrom.Configuration(builder.Configuration);
});

builder.Services.AddOpenTelemetry()
                .ConfigureResource(res => res.AddService("orderservice"))
                .WithTracing(tracing =>
                {
                    tracing.AddAspNetCoreInstrumentation(); // trace incoming http requests to Asp.Net core
                    tracing.AddHttpClientInstrumentation(); // trace out coming http client requests
                    
                    // export all tracing using Otlp (Open Telemetry Protocol)
                    tracing.AddOtlpExporter(options => 
                    {
                        options.Endpoint = new Uri("http://localhost:5341/ingest/otlp/v1/traces"); // OTEL_EXPORTER_OTLP_ENDPOINT
                        options.Protocol = OpenTelemetry.Exporter.OtlpExportProtocol.HttpProtobuf; // OTEL_EXPORTER_OTLP_PROTOCOL
                    });
                });

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddScoped<IOrderService, OrderService>();

builder.Services.AddScoped<IOrderRepository, OrderRepository>();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlite("Data Source = app.db");
});

builder.Services.AddHttpClient<IOrderService, OrderService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["PaymentService:BaseUrl"]!);
});

var app = builder.Build();

// user Serilog middleware
app.UseSerilogRequestLogging();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();
}

app.Run();