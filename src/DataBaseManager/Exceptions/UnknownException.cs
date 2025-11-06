namespace DataBaseManager.Exceptions;

public class UnknownException : Exception
{
    public UnknownException()
        : base("Unknown error has occurred")
    {
    }

    public UnknownException(string message)
        : base(message)
    {
    }

    public UnknownException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
