
interface IWetherService
{
    string GetWeatherInfo(string cityName);
}

class WetherService : IWetherService
{
    private IWeatherClient _weatherClient;

    public WetherService(IWeatherClient weatherClient)
    {
        _weatherClient = weatherClient;
    }

    public string GetWeatherInfo(string cityName)
    {
        var weatherClient = new WeatherClient();
        return weatherClient.GetWeatherInfo(cityName);
    }
}

interface IWeatherClient
{
    string GetWeatherInfo(string cityName);
}

class WeatherClient : IWeatherClient
{
    public string GetWeatherInfo(string cityName)
    {
        return $"Weather for {cityName} is {Random.Shared.Next(-10, 40)} C.";
    }
}
