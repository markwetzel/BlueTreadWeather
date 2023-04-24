namespace Common;

public class ForecastDTO
{
    // "Name" - this will be kept "City" for clarification in this demo 
    public string City { get; set; }

    // "Region" - this will be kept "State" for clarification in this demo 
    public string State { get; set; }
    public List<DayForecastDTO> DayForecasts { get; set; }
}