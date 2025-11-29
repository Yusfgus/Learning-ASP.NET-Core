
var builder = WebApplication.CreateBuilder(args);

// add required controller services
builder.Services.AddControllers();

var app = builder.Build();

// map the Controllers
app.MapControllers();

// Minimal Api
app.MapGet("/products", () =>
{
    return Results.Ok(new[]
    {
        "Product 1",
        "Product 2",
        "Product 3",
    });
});

app.MapGet("/route-table", (IServiceProvider sp) =>
{
    var endpoints = sp.GetRequiredService<EndpointDataSource>()
                        .Endpoints.Select(ep => ep.DisplayName);
    
    return Results.Ok(endpoints);
});

app.Run();
