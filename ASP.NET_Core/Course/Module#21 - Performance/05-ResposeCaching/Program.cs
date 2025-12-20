using ResponseCaching.Data;
using ResponseCaching.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddResponseCaching();

builder.Services.AddControllers();

builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlite("Data Source = app.db");
});

var app = builder.Build();

app.MapControllers();

app.UseResponseCaching();

app.MapGet("/api/products-mn", async (IProductService productService, int page = 1, int pageSize = 10) =>
{
    var PagedResult = await productService.GetProductsAsync(page, pageSize);

    Console.WriteLine("Minimal Endpoint visited");

    return Results.Ok(PagedResult);
});


app.MapGet("/api/products-mn/{productId:int}", async (int productId, IProductService productService) =>
{
    var response = await productService.GetProductByIdAsync(productId);

    Console.WriteLine($"Minimal Api visited... id = {productId}");

    return response is null
        ? Results.NotFound($"Product with Id '{productId}' not found")
        : Results.Ok(response);
});

app.Run();