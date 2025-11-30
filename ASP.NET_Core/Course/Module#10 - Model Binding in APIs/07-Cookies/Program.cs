
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
                .AddXmlSerializerFormatters();

var app = builder.Build();

app.MapControllers();

app.MapGet("/cookie-minimal", (HttpContext context) => 
{
    return Results.Ok(context.Request.Cookies["session-id"]);
});


app.Run();
