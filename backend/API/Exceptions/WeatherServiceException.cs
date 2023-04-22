public class WeatherServiceException : Exception
{
    public WeatherServiceException() { }

    public WeatherServiceException(string message)
        : base(message) { }

    public WeatherServiceException(string message, Exception inner)
        : base(message, inner) { }
}
