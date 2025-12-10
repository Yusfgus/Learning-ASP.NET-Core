
using System.Text.Json.Serialization;
using MinimalDataAnnotations.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

var app = builder.Build();

app.MapProductEndpoints();

app.Run();
