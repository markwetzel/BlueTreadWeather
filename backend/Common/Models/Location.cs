using System.Text.Json.Serialization;

namespace API;

public class Location
{
    [JsonPropertyName("name")]
    // This would ideally be named "City" in a US-only context
    public string Name { get; set; }

    [JsonPropertyName("region")]
    // This would ideally be named "State" in a US-only context
    public string Region { get; set; }
}
