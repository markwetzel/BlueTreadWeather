namespace API.Exceptions;

public class MissingApiKeyException : Exception
{
    public MissingApiKeyException()
    {
    }

    public MissingApiKeyException(string message)
        : base(message)
    {
    }

    public MissingApiKeyException(string message, Exception inner)
        : base(message, inner)
    {
    }
}
