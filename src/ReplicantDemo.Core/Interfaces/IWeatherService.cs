using ReplicantDemo.Core.Models;

namespace ReplicantDemo.Core.Interfaces;

public interface IWeatherService
{
    Task<WeatherForecast[]> GetForecastAsync();
}