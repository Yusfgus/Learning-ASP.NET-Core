
var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();


app.MapGet("/weather/city/{cityName}", (string cityName) =>
{
    var weatherService = new WetherService();
    string weatherInfo = weatherService.GetWeatherInfo(cityName);

    return Results.Ok(weatherInfo);
});


app.Run();
