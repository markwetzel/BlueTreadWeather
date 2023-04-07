using API.Exceptions;
using Common;
using Common.Options;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System.Net;
using System.Text.Json;

namespace API.Services;

public class WeatherService : IWeatherService
{
    private readonly ILogger<WeatherService> _logger;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;
    private readonly IMemoryCache _cache;
    private static readonly TimeSpan CacheDuration = TimeSpan.FromHours(1);

    private readonly string _weatherApiBaseUrl;
    private readonly string _weatherApiEndpoint;
    private readonly int _daysToForecast;

    public WeatherService(
        ILogger<WeatherService> logger,
        IHttpClientFactory httpClientFactory,
        IConfiguration configuration,
        IOptions<WeatherApiOptions> weatherApiOptions,
        IMemoryCache cache)
    {
        _logger = logger;
        _httpClientFactory = httpClientFactory;
        _configuration = configuration;
        _cache = cache;
        _weatherApiBaseUrl = weatherApiOptions.Value.BaseUrl;
        _weatherApiEndpoint = weatherApiOptions.Value.Endpoint;
        _daysToForecast = weatherApiOptions.Value.DaysToForecast;
    }

    public async Task<IEnumerable<ForecastDTO>> GetWeatherData(string[] cities)
    {
        try
        {
            var client = _httpClientFactory.CreateClient();
            var weatherApiKey = _configuration["Keys:WeatherApiKey"];

            if (string.IsNullOrEmpty(weatherApiKey))
            {
                throw new MissingApiKeyException("Weather API key is missing.");
            }

            var cacheKey = string.Join(',', cities);
            if (_cache.TryGetValue(cacheKey, out IEnumerable<ForecastDTO> cacheEntry))
            {
                return cacheEntry;
            }

            var tasks = cities.Select(async city =>
            {
                var url = $"{_weatherApiBaseUrl}{_weatherApiEndpoint}?key={weatherApiKey}&q={city}&days={_daysToForecast}";

                var response = await client.GetAsync(url).ConfigureAwait(false);

                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var forecastData = JsonSerializer.Deserialize<WeatherData>(json);

                var cityForecast = new ForecastDTO
                {
                    City = forecastData.Location.Name,
                    State = forecastData.Location.Region,
                    DayForecasts = forecastData.Forecast.ForecastDays
                        .Select(
                            forecastDay =>
                                new DayForecastDTO
                                {
                                    // ISO 8601 for easier client-side processing 
                                    Date = forecastDay.Date.ToString("O"),
                                    Text = forecastDay.Day.WeatherCondition.Description,
                                    Icon = forecastDay.Day.WeatherCondition.Icon,
                                    MinimumTemperatureCelsius = forecastDay.Day.MinimumTemperatureCelsius,
                                    MaximumTemperatureCelsius = forecastDay.Day.MaximumTemperatureCelsius,
                                    Humidity = forecastDay.Day.AverageHumidity
                                }
                        )
                        .ToList()
                };

                return cityForecast;
            });

            var forecasts = await Task.WhenAll(tasks);

            var cacheEntryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(CacheDuration);

            _cache.Set(cacheKey, forecasts, cacheEntryOptions);

            return forecasts;
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "Error getting weather data.");

            if (ex.StatusCode.HasValue)
            {
                if (ex.StatusCode.Value == HttpStatusCode.NotFound)
                {
                    throw new WeatherServiceException("City not found.", ex);
                }
                else if (ex.StatusCode.Value == HttpStatusCode.BadRequest)
                {
                    throw new WeatherServiceException("Invalid request.", ex);
                }
            }

            throw new WeatherServiceException(
                "An error occurred while fetching weather data.",
                ex
            );
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }
}
