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

namespace API.Tests;

public class WeatherServiceTests
{
    private readonly IConfiguration _configuration;
    private readonly string weatherApiKey;

    public WeatherServiceTests()
    {
        _configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddUserSecrets<WeatherServiceTests>()
            .Build();

        weatherApiKey = _configuration["Keys:WeatherApiKey"];

        Debug.WriteLine($"WeatherApi:DaysToForecast: {_configuration["WeatherApi:DaysToForecast"]}");

    }

    [Fact]
    public async Task GetWeatherData_ReturnsForecastData()
    {
        var loggerMock = new Mock<ILogger<WeatherService>>();
        var httpClientFactoryMock = new Mock<IHttpClientFactory>();
        var configurationMock = new Mock<IConfiguration>();
        var memoryCache = new MemoryCache(new MemoryCacheOptions());


        configurationMock
            .Setup(x => x["Keys:WeatherApiKey"])
            .Returns(weatherApiKey);

        httpClientFactoryMock
            .Setup(x => x.CreateClient(It.IsAny<string>()))
            .Returns(new HttpClient());

        var weatherApiOptions = Options.Create(new WeatherApiOptions
        {
            BaseUrl = _configuration["WeatherApi:BaseUrl"],
            Endpoint = _configuration["WeatherApi:Endpoint"],
            DaysToForecast = int.Parse(_configuration["WeatherApi:DaysToForecast"])
        });

        var weatherService = new WeatherService(loggerMock.Object, httpClientFactoryMock.Object, configurationMock.Object, weatherApiOptions, memoryCache);

        var result = await weatherService.GetWeatherData(new[] { "Tampa, FL", "Austin, TX", "Reno, NV" });

        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<ForecastDTO>>(result);
        Assert.Equal(3, result.Count());
    }

    [Fact]
    public async Task GetWeatherData_CachesResults()
    {
        var loggerMock = new Mock<ILogger<WeatherService>>();
        var httpClientFactoryMock = new Mock<IHttpClientFactory>();
        var configurationMock = new Mock<IConfiguration>();
        var memoryCache = new MemoryCache(new MemoryCacheOptions());

        configurationMock
            .Setup(x => x["Keys:WeatherApiKey"])
            .Returns(weatherApiKey);

        httpClientFactoryMock
            .Setup(x => x.CreateClient(It.IsAny<string>()))
            .Returns(new HttpClient());

        Debug.WriteLine(_configuration.ToString());

        var weatherApiOptions = Options.Create(new WeatherApiOptions
        {
            BaseUrl = _configuration["WeatherApi:BaseUrl"],
            Endpoint = _configuration["WeatherApi:Endpoint"],
            DaysToForecast = int.Parse(_configuration["WeatherApi:DaysToForecast"])
        });

        var weatherService = new WeatherService(loggerMock.Object, httpClientFactoryMock.Object, configurationMock.Object, weatherApiOptions, memoryCache);

        await weatherService.GetWeatherData(new[] { "Tampa, FL" });
        var cacheKey = "Tampa, FL";
        var cachedResult = memoryCache.TryGetValue(cacheKey, out IEnumerable<ForecastDTO> cachedForecasts);

        Assert.True(cachedResult);
        Assert.NotNull(cachedForecasts);
        Assert.Single(cachedForecasts);
        // This would be a strict check if given further development time 
        Assert.Contains(cachedForecasts, forecast => forecast.City.StartsWith("Tampa"));
    }

}


