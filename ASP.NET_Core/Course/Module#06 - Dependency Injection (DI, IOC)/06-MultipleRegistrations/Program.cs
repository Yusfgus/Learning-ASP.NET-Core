
using System.Text;
using Microsoft.Extensions.DependencyInjection.Extensions;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddTransient<IDependency, DependencyV1>();
builder.Services.AddTransient<IDependency, DependencyV1>(); // will add

builder.Services.AddTransient<IDependency, DependencyV2>();
builder.Services.TryAddTransient<IDependency, DependencyV2>(); // won't add due to repetition 


var app = builder.Build();


app.MapGet("/single", (IDependency dependency) =>
{
    string result = dependency.DoSomething();

    return Results.Ok(result);
});

app.MapGet("/multiple", (IEnumerable<IDependency> dependencies) =>
{
    string result = dependencies
                    .Select(d => d.DoSomething())
                    .Aggregate(
                        new StringBuilder(),
                        (sb, x) => sb.Append(x).Append(", "),
                        sb => sb.ToString().TrimEnd(',', ' ')
                    );

    return Results.Ok(result);
});

app.Map("/idependency-registrations", (IServiceProvider serviceProvider) =>
{
    var servicesRegisteredCount = serviceProvider.GetServices<IDependency>().Count();
    
    return Results.Ok(servicesRegisteredCount);
});


app.Run();
