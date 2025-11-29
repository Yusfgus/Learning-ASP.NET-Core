
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();

// Implicit Binding
app.MapGet("/product-minimal-1/{id}", 
            (int id) => $"Product {id}");

// Explicit Binding
app.MapGet("/product-minimal-2/{id}", 
            ([FromRoute(Name="id")] int identifier) => $"Product {identifier}");

// From Query
app.MapGet("/product-minimal-3/{id}", 
            ([FromQuery] int id) => $"Product {id}");


app.Run();
