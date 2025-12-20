using ResponseCompression.Data;
using ResponseCompression.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.ResponseCompression;
using System.IO.Compression;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
    options.Providers.Add<GzipCompressionProvider>(); // by default
    options.Providers.Add<BrotliCompressionProvider>();
    options.MimeTypes = 
    [
        "application/json",
        "application/xml",
        "text/plain",
        "text/html",
    ];
});

builder.Services.Configure<GzipCompressionProviderOptions>(options =>
{
    options.Level = CompressionLevel.Fastest;
});

builder.Services.Configure<BrotliCompressionProviderOptions>(options =>
{
    options.Level = CompressionLevel.Fastest;
});

builder.Services.AddControllers();

builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlite("Data Source = app.db");
});

var app = builder.Build();

app.UseResponseCompression();

app.MapControllers();


app.MapGet("/api/products-mn", async (IProductService productService, int page = 1, int pageSize = 10) =>
{
    var PagedResult = await productService.GetProductsAsync(page, pageSize);
    return Results.Ok(PagedResult);
});


app.MapGet("/api/products-mn/{productId:int}", async (int productId, IProductService productService) =>
{
    var response = await productService.GetProductByIdAsync(productId);
    return response is null
        ? Results.NotFound($"Product with Id '{productId}' not found")
        : Results.Ok(response);
});

app.Run();