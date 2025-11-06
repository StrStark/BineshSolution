namespace DataBaseManager.Exceptions;
public class ServerConnectionException : KnownException
{
    public ServerConnectionException()
        : base("Unable to connect to server.")
    {
    }

    public ServerConnectionException(string message)
        : base(message)
    {
    }

    public ServerConnectionException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
