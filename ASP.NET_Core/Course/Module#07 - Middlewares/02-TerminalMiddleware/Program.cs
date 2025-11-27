
var builder = WebApplication.CreateBuilder(args);

// DI Container ( Configuring Dependencies )

var app = builder.Build();

// Middleware setup

// public delegate Task RequestDelegate(HttpContext context)

app.Use(async (HttpContext context, RequestDelegate next) =>
{
    await context.Response.WriteAsync("Middleware #1 \n");
    await next(context);
});

// Adds a terminal middleware to the request pipeline
// Doesn't call next()
// public static void Run(this IApplicationBuilder app, RequestDelegate handler)
// no Use, Map, Run after it will execute
app.Run(async (HttpContext context) =>
{
    await context.Response.WriteAsync("Terminal middleware)");
});

app.Use(async (HttpContext context, RequestDelegate next) =>
{
    await context.Response.WriteAsync("Middleware #2 \n");
    await next(context);
});

app.Run();
