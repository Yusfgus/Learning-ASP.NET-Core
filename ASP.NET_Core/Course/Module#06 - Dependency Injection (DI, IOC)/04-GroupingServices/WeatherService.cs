
interface IWeatherService
{
    string GetWeatherInfo(string cityName);
}

class WeatherService : IWeatherService
{
    private IWeatherClient _weatherClient;

    public WeatherService(IWeatherClient weatherClient)
    {
        _weatherClient = weatherClient;
    }

    public string GetWeatherInfo(string cityName)
    {
        return _weatherClient.GetWeatherInfo(cityName);
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
