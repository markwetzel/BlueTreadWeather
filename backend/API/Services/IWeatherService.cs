using Common;

namespace API.Services;

public interface IWeatherService
{
    Task<IEnumerable<ForecastDTO>> GetWeatherData(string[] cities);
}
