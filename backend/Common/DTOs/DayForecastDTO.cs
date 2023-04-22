namespace Common;

public class DayForecastDTO
{
    public string Date { get; set; }
    public string Text { get; set; }
    public string Icon { get; set; }
    public double MinimumTemperatureCelsius { get; set; }
    public double MaximumTemperatureCelsius { get; set; }
    public double Humidity { get; set; }
}