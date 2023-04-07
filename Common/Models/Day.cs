using System.Text.Json.Serialization;

namespace API;

public class Day
{
    [JsonPropertyName("maxtemp_c")]
    public double MaximumTemperatureCelsius { get; set; }

    [JsonPropertyName("mintemp_c")]
    public double MinimumTemperatureCelsius { get; set; }

    [JsonPropertyName("avghumidity")]
    public double AverageHumidity { get; set; }

    [JsonPropertyName("condition")]
    public Condition WeatherCondition { get; set; }
}
