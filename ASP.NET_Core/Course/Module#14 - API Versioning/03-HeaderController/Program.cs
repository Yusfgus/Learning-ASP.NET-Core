
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using HeaderController.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddSingleton<ProductRepository>();

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0); // default version
    options.AssumeDefaultVersionWhenUnspecified = true; // assume default version
    options.ReportApiVersions = true;
    options.ApiVersionReader = new HeaderApiVersionReader("api-version"); // /api/products/20
                                                                          // api-version: 1.0
});

var app = builder.Build();

app.MapControllers();

app.Run();
