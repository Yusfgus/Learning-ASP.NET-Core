
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
                .AddXmlSerializerFormatters();

var app = builder.Build();

app.MapControllers();

// Post and Put request by default send data in the Body so no need for [FromBody]
// No more than one [FromBody] is allowed in a single request
app.MapPost("product-minimal", ([FromBody] ProductRequest request) =>
{
    return Results.Ok(request);
});


app.Run();
