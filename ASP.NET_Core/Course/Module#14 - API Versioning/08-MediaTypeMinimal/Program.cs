
using Asp.Versioning;
using Asp.Versioning.Builder;
using MediaTypeMinimal.Data;
using MediaTypeMinimal.Endpoints.V1;
using MediaTypeMinimal.Endpoints.V2;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<ProductRepository>();

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0); // default version
    options.AssumeDefaultVersionWhenUnspecified = true; // assume default version
    options.ReportApiVersions = true;
    options.ApiVersionReader = new MediaTypeApiVersionReader("v");  // /api/products
                                                                    // Accept: application/json;v=1.0
});

var app = builder.Build();

ApiVersionSet apiVersionSet = app.NewApiVersionSet()
                                .HasApiVersion(new ApiVersion(1.0))
                                .HasApiVersion(new ApiVersion(2, 0))
                                .ReportApiVersions()
                                .Build();


app.MapProductEndpointsV1(apiVersionSet);
app.MapProductEndpointsV2(apiVersionSet);

app.Run();