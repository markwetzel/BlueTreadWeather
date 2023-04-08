using System.Text.Json.Serialization;

namespace API;

public class Forecast
{
    [JsonPropertyName("forecastday")]
    public List<ForecastDay> ForecastDays { get; set; }
}
