
var builder = WebApplication.CreateBuilder(args);

// Using lifetime specific extension (used 95% of the time - less noise, same result)
builder.Services.AddTransient<IWeatherClient, WeatherClient>();

// Using Service Descriptor (when doing meta-programming, custom factories, or working with reflection)
builder.Services.Add(new ServiceDescriptor(
    typeof(IWeatherService),
    typeof(WeatherService),
    ServiceLifetime.Transient
));


var app = builder.Build();


app.MapGet("/weather/city/{cityName}", (string cityName, IWeatherService weatherService) =>
{
    string weatherInfo = weatherService.GetWeatherInfo(cityName);

    return Results.Ok(weatherInfo);
});


app.Run();
