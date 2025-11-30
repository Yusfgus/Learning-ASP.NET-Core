using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();

app.MapGet("/product-minimal", 
([FromHeader(Name = "X-Api-Version")] string apiVersion, [FromHeader] string language) =>
{
    return $"Api Version: {apiVersion}\nLanguage: {language}";
});



app.Run();
