
var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();


app.MapGet("/weather/city/{cityName}", (string cityName) =>
{
    IWeatherClient weatherClient = new WeatherClient(); // abstraction

    IWetherService weatherService = new WetherService(weatherClient); // abstraction

    string weatherInfo = weatherService.GetWeatherInfo(cityName);

    return Results.Ok(weatherInfo);
});


app.Run();
