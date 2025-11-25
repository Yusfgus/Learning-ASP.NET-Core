
class WetherService
{
    public string GetWeatherInfo(string cityName)
    {
        var weatherClient = new WeatherClient();
        return weatherClient.GetWeatherInfo(cityName);
    }
}

class WeatherClient
{
    public string GetWeatherInfo(string cityName)
    {
        var http = new HttpClient();
        // some external http call
        return $"Weather for {cityName} is {Random.Shared.Next(-10, 40)} C.";
    }
}
