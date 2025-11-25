
var builder = WebApplication.CreateBuilder(args);

// DI ( IoC Container )
// Service Registration

builder.Services.AddWeatherServices();

var app = builder.Build();


app.MapGet("/weather/city/{cityName}", (string cityName, IWeatherService weatherService) =>
{
    string weatherInfo = weatherService.GetWeatherInfo(cityName);

    return Results.Ok(weatherInfo);
});


app.Run();
