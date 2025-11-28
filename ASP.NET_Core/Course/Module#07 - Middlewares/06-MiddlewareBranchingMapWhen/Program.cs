
var builder = WebApplication.CreateBuilder(args);

// DI Container ( Configuring Dependencies )

var app = builder.Build();

// Middleware setup

app.MapWhen(
    (HttpContext context) => 
    { 
        return context.Request.Path.Equals("/checkout", StringComparison.OrdinalIgnoreCase) 
                && context.Request.Query["mode"] == "new";
    },

    (IApplicationBuilder b) =>
    {
        b.Run(async (HttpContext context) =>
        {
            await context.Response.WriteAsync("Modern checkout processing pipeline");
        });
    }
);

// Note that order matters => (MapWhen + Map) != (Map + MapWhen)

app.Map("/checkout", b =>
{
    b.Run(async (HttpContext context) =>
    {
        await context.Response.WriteAsync("Legacy checkout processing pipeline");
    });
});


app.Run(async (HttpContext context) =>
{
    await context.Response.WriteAsync("Terminal Middleware");
});


app.Run();

