
var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.MapGet("/authors/{author}", async (string author, HttpContext context) =>
{
    var data = new
    {
        Id = context.TraceIdentifier,
        context.Request.Scheme,
        context.Request.Host,
        context.Request.Method,
        context.Request.Path,
        context.Request.Query,
        context.Request.Headers,
        context.Request.RouteValues,
        Body = await new StreamReader(context.Request.Body).ReadToEndAsync()
    };

    return Results.Ok(data);
});

app.Run();
