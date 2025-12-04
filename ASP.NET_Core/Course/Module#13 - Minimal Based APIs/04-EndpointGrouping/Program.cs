
using EndpointGrouping.Data;
using EndpointGrouping.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<ProductRepository>();

var app = builder.Build();

app.MapProductEndpoints();

app.Run();
