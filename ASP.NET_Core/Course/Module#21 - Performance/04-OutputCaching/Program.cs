using OutputCaching.Data;
using OutputCaching.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOutputCache(options =>
{
    // options.DefaultExpirationTimeSpan = TimeSpan.FromMinutes(10);
    // options.MaximumBodySize = 64 * 1024; // 64 KB
    // options.SizeLimit = 100 * 1024 * 1024; // 100 MB
    // options.UseCaseSensitivePaths = false;

    options.AddPolicy("Single-Product", builder =>
    {
        builder.Expire(TimeSpan.FromSeconds(10));
        builder.SetVaryByRouteValue(["productId"]);
        builder.Tag(["products"]);
    });
});

builder.Services.AddControllers();

builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlite("Data Source = app.db");
});

var app = builder.Build();

app.UseOutputCache();

app.MapControllers();

app.MapGet("/api/products-mn", async (IProductService productService, int page = 1, int pageSize = 10) =>
{
    var PagedResult = await productService.GetProductsAsync(page, pageSize);

    Console.WriteLine("Minimal Endpoint visited");

    return Results.Ok(PagedResult);
})
.CacheOutput(options =>
{
    options.Expire(TimeSpan.FromSeconds(10));
    options.SetVaryByQuery(["page", "pageSize"]);
    // options.Tag(["products"]); // not working
});


app.MapGet("/api/products-mn/{productId:int}", async (int productId, IProductService productService) =>
{
    var response = await productService.GetProductByIdAsync(productId);

    Console.WriteLine($"Minimal Api visited... id = {productId}");

    return response is null
        ? Results.NotFound($"Product with Id '{productId}' not found")
        : Results.Ok(response);
})
// .CacheOutput(options => 
// {
//     options.Expire(TimeSpan.FromSeconds(10));
//     options.SetVaryByRouteValue(["productId"]);
// })
.CacheOutput("Single-Product") // Policy
;

app.Run();