
using TraditionalTimer.BackgroundJobs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHostedService<BlobStorageCleanupBackground>();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
