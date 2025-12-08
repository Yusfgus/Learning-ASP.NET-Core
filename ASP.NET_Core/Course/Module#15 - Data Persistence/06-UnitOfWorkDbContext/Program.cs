
using UnitOfWorkDbContext.Data;
using UnitOfWorkDbContext.Endpoints;
using UnitOfWorkDbContext.Interfaces;
using UnitOfWorkDbContext.Repositories;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.Configure<JsonOptions>(options =>
{
    // options.SerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault;
    options.SerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
});

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlite("Data Source = app.db");
});

var app = builder.Build();

app.MapProductEndpoints();

app.Run();
