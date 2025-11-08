using System.Net;

namespace BineshSoloution.Exceptions;

public class RestException : KnownException
{
    public RestException()
        : base("An error occurred while communicating with server")
    {
    }

    public RestException(string message)
        : base(message)
    {
    }

    public RestException(string message, Exception? innerException)
        : base(message, innerException)
    {
    }
    public virtual HttpStatusCode StatusCode => HttpStatusCode.InternalServerError;
}
