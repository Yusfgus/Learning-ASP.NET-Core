using InMemoryCaching.Data;
using InMemoryCaching.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddMemoryCache(options => options.SizeLimit = 100); // register 100 unit (Byte, MB, GB, ...)

// Invalidate cache on any operation that changes the data it represents.
//  - Create → invalidate
//  - Update → invalidate
//  - Delete → invalidate
//  - Read → cache

builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlite("Data Source = app.db");
});

var app = builder.Build();

app.MapControllers();

app.Run();