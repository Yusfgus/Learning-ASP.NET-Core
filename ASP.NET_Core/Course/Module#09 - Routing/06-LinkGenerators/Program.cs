
var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.MapGet("/order/{id:int}", (int id, LinkGenerator link, HttpContext context) =>
{
    // link to 'edit' endpoint
    string? editUrl = link.GetUriByName
                    (
                        endpointName: "EditOrder",
                        values: new { id },
                        scheme: context.Request.Scheme,
                        host: context.Request.Host
                    );

    return Results.Ok(new
    {
        OrderId = id,
        Status = "PENDING",
        _links = new
        {
            self = new { href = context.Request.Path },
            edit = new { href = editUrl, Method = "PUT" }
        }
    });
});

app.MapPut("/order/{id:int}", (int id) =>
{
    // update order
    return Results.NoContent();
})
.WithName("EditOrder");

app.Run();
