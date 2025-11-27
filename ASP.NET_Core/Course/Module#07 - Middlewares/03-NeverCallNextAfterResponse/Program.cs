
var builder = WebApplication.CreateBuilder(args);

// DI Container ( Configuring Dependencies )

var app = builder.Build();

// Middleware setup

app.Use(async (HttpContext context, RequestDelegate next) =>
{
    context.Response.Headers.Append("X-Hdr1", "val1"); // modify header
    context.Response.StatusCode = StatusCodes.Status401Unauthorized; // modify status code

    await context.Response.WriteAsync("Middleware #1 \n"); // response has begun

    // context.Response.Headers.Append("X-Hdr1", "val1"); // Exception: Headers are read-only, response has already started.
    // context.Response.StatusCode = StatusCodes.Status401Unauthorized; // Exception: StatusCode cannot be set because the response has already started.

    await next(context);
});

app.Use(async (HttpContext context, RequestDelegate next) =>
{
    // context.Response.Headers.Append("X-Hdr1", "val2"); // Exception: Headers are read-only, response has already started.
    // context.Response.StatusCode = StatusCodes.Status404NotFound; // Exception: StatusCode cannot be set because the response has already started.

    await next(context);
});

app.Use(async (HttpContext context, RequestDelegate next) =>
{
    if(context.Response.HasStarted == false)
    {
        context.Response.Headers.Append("X-Hdr1", "val3"); // Exception: Headers are read-only, response has already started.
        context.Response.StatusCode = StatusCodes.Status203NonAuthoritative; // Exception: StatusCode cannot be set because the response has already started.
    }

    await next(context);
});


app.Run();
