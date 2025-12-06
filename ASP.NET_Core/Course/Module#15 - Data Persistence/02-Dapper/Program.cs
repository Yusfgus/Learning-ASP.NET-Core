
using System.Data;
using Dapper;
using Dapper.Data;
using Dapper.Data.Handlers;
using Dapper.Endpoints;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.Data.Sqlite;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ProductRepository>();

builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault;
});

builder.Services.AddScoped<IDbConnection>(_ =>
    new SqliteConnection("Data Source=app.db"));

var app = builder.Build();

using var scope = app.Services.CreateScope();

var db = scope.ServiceProvider.GetRequiredService<IDbConnection>();

DatabaseInitializer.Initialize(db);

SqlMapper.AddTypeHandler(new GuidHandler());

app.MapProductEndpoints();

app.Run();