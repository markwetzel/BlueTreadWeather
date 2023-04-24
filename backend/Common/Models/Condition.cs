using System.Text.Json.Serialization;

namespace API;

public class Condition
{
    [JsonPropertyName("text")]
    public string Description { get; set; }

    [JsonPropertyName("icon")]
    public string Icon { get; set; }
}