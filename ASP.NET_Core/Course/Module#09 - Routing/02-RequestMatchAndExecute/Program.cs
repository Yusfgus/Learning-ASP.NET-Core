
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var app = builder.Build();

//Adds rout matching middleware to the pipeline
app.UseRouting();

// Adds endpoint execution to the middleware pipeline
// Runs the delegate associated with the selected endpoint
#pragma warning disable ASP0014 // Suggest using top level route registrations
app.UseEndpoints(ep =>
{
    // map the Controllers
    ep.MapControllers();

    // Minimal Api
    ep.MapGet("/products", () =>
    {
        return Results.Ok(new[]
        {
            "Product 1",
            "Product 2",
            "Product 3",
        });
    });
});
#pragma warning restore ASP0014 // Suggest using top level route registrations

app.Run();
