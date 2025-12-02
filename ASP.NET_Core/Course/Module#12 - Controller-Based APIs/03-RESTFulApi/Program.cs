
using RESTFulApi.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddSingleton<ProductRepository>();

var app = builder.Build();

app.MapControllers();

app.Run();
