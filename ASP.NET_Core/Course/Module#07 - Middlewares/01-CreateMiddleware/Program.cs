
var builder = WebApplication.CreateBuilder(args);

// DI Container ( Configuring Dependencies )

var app = builder.Build();

// Middleware setup

// public IApplicationBuilder Use(Func<RequestDelegate, RequestDelegate> middleware)
// public delegate Task RequestDelegate(HttpContext context)

// Middleware do nothing
app.Use((RequestDelegate next) => next);

// Middleware intercept httpcontext object (original way)
app.Use((RequestDelegate next) =>
{
    return async (HttpContext context) =>
    {
        await context.Response.WriteAsync("MW #2 \n");
        await next(context);
    };
});

// Middleware intercept httpcontext object (using extension method)
app.Use(async (HttpContext context, RequestDelegate next) =>
{
    await context.Response.WriteAsync("MW #3 \n");
    await next(context);
});

// doesn't call next
app.Use(async (HttpContext context, RequestDelegate next) =>
{
    await context.Response.WriteAsync("MW #4 \n");
    // await next(context);
});

// unreachable
app.Use(async (HttpContext context, RequestDelegate next) =>
{
    await context.Response.WriteAsync("MW #5 \n");
    await next(context);
});

app.Run();
