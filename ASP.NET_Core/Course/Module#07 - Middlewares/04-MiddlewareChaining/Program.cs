
var builder = WebApplication.CreateBuilder(args);

// DI Container ( Configuring Dependencies )

var app = builder.Build();

// Middleware setup

app.Use(async (HttpContext context, Func<Task> next) =>
{
    await context.Response.WriteAsync("MW 1 Before\n");
    await next();
    await context.Response.WriteAsync("MW 1 After\n");
});

app.Use(async (HttpContext context, Func<Task> next) =>
{
    await context.Response.WriteAsync("\tMW 2 Before\n");
    await next();
    await context.Response.WriteAsync("\tMW 2 After\n");
});

app.Use(async (HttpContext context, Func<Task> next) =>
{
    await context.Response.WriteAsync("\t\tMW 3 Before\n");
    await next();
    await context.Response.WriteAsync("\t\tMW 3 After\n");
});


app.Run();
