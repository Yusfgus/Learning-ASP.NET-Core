
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddKeyedTransient<IDependency, DependencyV1>("v1");

builder.Services.AddKeyedTransient<IDependency, DependencyV2>("v2");

var app = builder.Build();


app.MapGet("/v1", ([FromKeyedServices("v1")] IDependency dependency) =>
{
    string result = dependency.DoSomething();
    return Results.Ok(result);
});

app.MapGet("/v2", ([FromKeyedServices("v2")] IDependency dependency) =>
{
    string result = dependency.DoSomething();
    return Results.Ok(result);
});

app.Run();
