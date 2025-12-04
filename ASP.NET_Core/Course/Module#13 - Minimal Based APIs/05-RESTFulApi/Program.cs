
using Microsoft.AspNetCore.Http.Json;
using RESTFulApi.Data;
using RESTFulApi.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<ProductRepository>();

builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault;
});

var app = builder.Build();

app.MapProductEndpoints();

app.Run();
