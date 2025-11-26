
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ServiceA>();
builder.Services.AddScoped<ServiceB>();

// // created ones per program
// builder.Services.AddSingleton<RequestTracker>();

// // created ones per request
// builder.Services.AddScoped<RequestTracker>();

// create new per use
builder.Services.AddTransient<RequestTracker>();

var app = builder.Build();

app.MapGet("/check", (ServiceA serviceA, ServiceB serviceB) =>
{
    return Results.Ok(
        new
        {
            A = serviceA.GetInfo(),
            B = serviceB.GetInfo(),
        }
    );
});


app.Run();

public class RequestTracker
{
    public string TrackerId = Guid.NewGuid().ToString()[..8];
}

public class ServiceA(RequestTracker tracker)
{
    public string GetInfo() => $"A => {tracker.TrackerId}";
}

public class ServiceB(RequestTracker tracker)
{
    public string GetInfo() => $"B => {tracker.TrackerId}";
}
