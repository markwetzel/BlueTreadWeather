using System.Text.Json.Serialization;

namespace API;

public class WeatherData
{
    [JsonPropertyName("location")]
    public Location Location { get; set; }

    [JsonPropertyName("forecast")]
    public Forecast Forecast { get; set; }
}
