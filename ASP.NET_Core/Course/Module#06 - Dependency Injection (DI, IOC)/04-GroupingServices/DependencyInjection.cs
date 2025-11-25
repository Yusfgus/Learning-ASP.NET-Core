namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddWeatherServices()
        {
            services.AddTransient<IWeatherClient, WeatherClient>();

            services.AddTransient<IWeatherService, WeatherService>();

            return services;
        }
    }
}
