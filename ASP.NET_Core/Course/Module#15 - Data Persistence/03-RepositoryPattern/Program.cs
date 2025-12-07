
using System.Data;
using Dapper;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;
using RepositoryPattern.Data;
using RepositoryPattern.Data.Handlers;
using RepositoryPattern.Interfaces;
using RepositoryPattern.Endpoints;
using RepositoryPattern.Repositories;

var builder = WebApplication.CreateBuilder(args);

// EF Core
// builder.Services.AddScoped<IProductRepository, EFProductRepository>();

// Dapper
// builder.Services.AddScoped<IProductRepository, DapperProductRepository>();

// Using Factory Instantiation
builder.Services.AddScoped<IProductRepository>(sp =>
{
    var config = sp.GetRequiredService<IConfiguration>();
    return config["DatabaseProvider"] switch
    {
        "EF_Core" => new EFProductRepository(sp.GetService<AppDbContext>()!),
        "Dapper" => new DapperProductRepository(sp.GetService<IDbConnection>()!),
        _ => new EFProductRepository(sp.GetService<AppDbContext>()!),
    };
});


// both
builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault;
});

// EF Core
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlite("Data Source = app.db");
});

// Dapper
builder.Services.AddScoped<IDbConnection>(_ => new SqliteConnection("Data Source=app.db"));

// both
var app = builder.Build();

// Dapper
SqlMapper.AddTypeHandler(new GuidHandler());

// both
app.MapProductEndpoints();

// both
app.Run();