using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();

app.MapGet("/product-minimal", 
([FromForm] string name, [FromForm] decimal price) =>
{
    return new {name, price};
})
.DisableAntiforgery();


app.Run();
