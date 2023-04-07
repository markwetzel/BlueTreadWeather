using System.Text.Json.Serialization;

namespace API;

public class ForecastDay
{
    [JsonPropertyName("date")]
    public DateTime Date { get; set; }

    [JsonPropertyName("day")]
    public Day Day { get; set; }
}