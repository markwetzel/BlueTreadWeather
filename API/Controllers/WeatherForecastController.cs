using API.Exceptions;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IWeatherService _weatherService;

    public WeatherForecastController(
        ILogger<WeatherForecastController> logger,
        IWeatherService weatherService
    )
    {
        _logger = logger;
        _weatherService = weatherService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Get([FromQuery(Name = "city")] string[] cities)
    {
        try
        {
            var weatherData = await _weatherService.GetWeatherData(cities);
            Response.Headers.Add("Cache-Control", $"public, max-age={3600}");
            return new OkObjectResult(weatherData);
        }
        catch (MissingApiKeyException ex)
        {
            // Log the exception
            _logger.LogError(ex, "Missing API key: {Message}", ex.Message);

            // Return a user-friendly error message
            return StatusCode(500, new { Error = "An internal server error occurred. Please contact the administrator." });
        }
        catch (Exception ex)
        {
            // Log the exception
            _logger.LogError(ex, "An error occurred while fetching weather data: {Message}", ex.Message);

            // Return a generic error message
            return StatusCode(500, new { Error = "An error occurred while fetching weather data. Please try again later." });
        }
    }
}
