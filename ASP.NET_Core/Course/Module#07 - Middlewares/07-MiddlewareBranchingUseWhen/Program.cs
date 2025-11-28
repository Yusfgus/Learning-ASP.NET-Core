
var builder = WebApplication.CreateBuilder(args);

// DI Container ( Configuring Dependencies )

var app = builder.Build();

// Middleware setup

app.UseWhen(
    (HttpContext context) => 
    { 
        return context.Request.Path.Equals("/branch1", StringComparison.OrdinalIgnoreCase);
    },

    (IApplicationBuilder b) =>
    {
        b.Use(async (HttpContext context, Func<Task> next) =>
        {
            await context.Response.WriteAsync("Middleware Branch 1\n");
            await next();
        });
    }
);

app.Run(async (HttpContext context) =>
{
    await context.Response.WriteAsync("Terminal Middleware Main Pipeline");
});


app.Run();

