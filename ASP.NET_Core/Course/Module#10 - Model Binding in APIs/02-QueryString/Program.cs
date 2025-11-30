using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();

// Implicit Binding
app.MapGet("/product-minimal-1", 
            (int page, int pageSize) => $"Showing {pageSize} items of page # {page}");

// Explicit Binding
app.MapGet("/product-minimal-2", 
            ([FromQuery(Name = "page")]int p, [FromQuery(Name = "pageSize")] int ps) => $"Showing {p} items of page # {ps}");

// AsParameters
app.MapGet("product-minimal-3", 
            ([AsParameters] SearchRequest request) => request);

// Array
app.MapGet("product-minimal-4", 
            (int[] ids) => string.Join(", ", ids));

// Custom Parsing
app.MapGet("product-minimal-5", 
            (DateRangeQuery dateRange) => dateRange);


app.Run();
