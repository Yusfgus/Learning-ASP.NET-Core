using System.Text.Json.Serialization;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// // add Serilog provider with the other default providers
// builder.Services.AddSerilog();

// add Serilog provider only
builder.Host.UseSerilog((context, loggerConfig) =>
{
    loggerConfig.ReadFrom.Configuration(builder.Configuration);
});

builder.Services.AddOpenTelemetry()
                .ConfigureResource(res => res.AddService("paymentservice"))
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
                })
                .WithMetrics(metrics =>
                {
                    metrics.AddAspNetCoreInstrumentation();
                    metrics.AddHttpClientInstrumentation();

                    metrics.AddOtlpExporter();
                    metrics.AddPrometheusExporter(); // /metrics
                });

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

var app = builder.Build();

app.UseSerilogRequestLogging();

app.UseOpenTelemetryPrometheusScrapingEndpoint();

app.MapControllers();

app.Run();
