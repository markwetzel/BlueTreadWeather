using API.Services;
using Moq;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Common;
using Microsoft.Extensions.Caching.Memory;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;
using Microsoft.Extensions.Options;
using Common.Options;
using System.Diagnostics;

using Common.Utils;

namespace API.Tests;

public class WeatherServiceTests
{
    private readonly IConfiguration _configuration;
    private readonly string weatherApiKey;

    public WeatherServiceTests()
    {
        string envPath = Path.GetFullPath(
            Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", ".env")
        );
        DotEnv.Load(envPath);

        _configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables()
            .Build();

		// Get the key from the environment variable
		weatherApiKey = _configuration["WeatherApiKey"];

        Debug.WriteLine(
            $"WeatherApi:DaysToForecast: {_configuration["WeatherApi:DaysToForecast"]}"
        );
    }

    [Fact]
    public async Task GetWeatherData_ReturnsForecastData()
    {
        var loggerMock = new Mock<ILogger<WeatherService>>();
        var httpClientFactoryMock = new Mock<IHttpClientFactory>();
        var configurationMock = new Mock<IConfiguration>();
        var memoryCache = new MemoryCache(new MemoryCacheOptions());

        configurationMock.Setup(config => config["WeatherApiKey"]).Returns(weatherApiKey);

        httpClientFactoryMock
            .Setup(http => http.CreateClient(It.IsAny<string>()))
            .Returns(new HttpClient());

        var weatherApiOptions = Options.Create(
            new WeatherApiOptions
            {
                BaseUrl = _configuration["WeatherApi:BaseUrl"],
                Endpoint = _configuration["WeatherApi:Endpoint"],
                DaysToForecast = int.Parse(_configuration["WeatherApi:DaysToForecast"])
            }
        );

        var weatherService = new WeatherService(
            loggerMock.Object,
            httpClientFactoryMock.Object,
            configurationMock.Object,
            weatherApiOptions,
            memoryCache
        );

        var weatherResult = await weatherService.GetWeatherData(
            new[] { "Tampa, FL", "Austin, TX", "Reno, NV" }
        );

        Assert.NotNull(weatherResult);
        Assert.IsAssignableFrom<IEnumerable<ForecastDTO>>(weatherResult);
        Assert.Equal(3, weatherResult.Count());
    }

    [Fact]
    public async Task GetWeatherData_CachesResults()
    {
        var loggerMock = new Mock<ILogger<WeatherService>>();
        var httpClientFactoryMock = new Mock<IHttpClientFactory>();
        var configurationMock = new Mock<IConfiguration>();
        var memoryCache = new MemoryCache(new MemoryCacheOptions());

        configurationMock.Setup(config => config["WeatherApiKey"]).Returns(weatherApiKey);

        httpClientFactoryMock
            .Setup(http => http.CreateClient(It.IsAny<string>()))
            .Returns(new HttpClient());

        Debug.WriteLine(_configuration.ToString());

        var weatherApiOptions = Options.Create(
            new WeatherApiOptions
            {
                BaseUrl = _configuration["WeatherApi:BaseUrl"],
                Endpoint = _configuration["WeatherApi:Endpoint"],
                DaysToForecast = int.Parse(_configuration["WeatherApi:DaysToForecast"])
            }
        );

        var weatherService = new WeatherService(
            loggerMock.Object,
            httpClientFactoryMock.Object,
            configurationMock.Object,
            weatherApiOptions,
            memoryCache
        );

        await weatherService.GetWeatherData(new[] { "Tampa, FL" });
        var cacheKey = "Tampa, FL";
        var cachedResult = memoryCache.TryGetValue(
            cacheKey,
            out IEnumerable<ForecastDTO> cachedForecasts
        );

        Assert.True(cachedResult);
        Assert.NotNull(cachedForecasts);
        Assert.Single(cachedForecasts);
        // This would be a strict check if given further development time
        Assert.Contains(cachedForecasts, forecast => forecast.City.StartsWith("Tampa"));
    }
}
