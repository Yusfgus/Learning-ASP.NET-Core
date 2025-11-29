
var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

// before app.UseRouting()
app.Use(async(HttpContext context, Func<Task> next) =>
{
    var endpoint = context.GetEndpoint()?.DisplayName ?? "No Endpoint defined!!";
    Console.WriteLine($"Middleware 1, Endpoint ({endpoint})");
    await next();
});

app.UseRouting();

// after app.UseRouting();
app.Use(async(HttpContext context, Func<Task> next) =>
{
    var endpoint = context.GetEndpoint()?.DisplayName ?? "No Endpoint defined!!";
    Console.WriteLine($"Middleware 2, Endpoint ({endpoint})");
    await next();
});


app.MapGet("/products", () =>
{
    return Results.Ok(new[]
    {
        "Product 1",
        "Product 2",
    });
});

app.Run();
