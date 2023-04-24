namespace Common.Options;

public class WeatherApiOptions
{
    public string BaseUrl { get; set; }
    public string Endpoint { get; set; }
    public int DaysToForecast { get; set; }
}