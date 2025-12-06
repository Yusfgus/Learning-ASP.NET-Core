
using EFCoreCodeFirst.Data;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// builder.Services.AddScoped<ProductRepository>();
builder.Services.AddScoped<ProductRepositoryAsync>();

// builder.Services.AddSingleton<ProductRepository>();  
// System.InvalidOperationException: Cannot consume scoped service 'Microsoft.EntityFrameworkCore.DbContextOptions`1[EFCoreCodeFirst.Data.AppDbContext]' 
// from singleton 'EFCoreCodeFirst.Data.ProductRepository'.

builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault;
});

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlite("Data Source = app.db");
});

var app = builder.Build();

// app.MapProductEndpoints();
app.MapProductEndpointsAsync();

app.Run();
