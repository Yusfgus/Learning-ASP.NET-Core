
using Asp.Versioning;
using HeaderMinimal.Data;
using HeaderMinimal.Endpoints.V1;
using HeaderMinimal.Endpoints.V2;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<ProductRepository>();

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0); // default version
    options.AssumeDefaultVersionWhenUnspecified = true; // assume default version
    options.ReportApiVersions = true;
    options.ApiVersionReader = new HeaderApiVersionReader("api-version"); // /api/products
                                                                          // api-version: 1.0
});

var app = builder.Build();

var apiVersionSet = app.NewApiVersionSet()
    .HasApiVersion(new ApiVersion(1.0))
    .HasApiVersion(new ApiVersion(2, 0))
    .ReportApiVersions()
    .Build();


app.MapProductEndpointsV1(apiVersionSet);
app.MapProductEndpointsV2(apiVersionSet);

app.Run();