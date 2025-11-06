using System.Net;

namespace DataBaseManager.Exceptions;

public class UnauthorizedException : RestException
{
    public UnauthorizedException()
        : base("Your request lacks valid authentication credentials")
    {
    }

    public UnauthorizedException(string message)
        : base(message)
    {
    }

    public UnauthorizedException(string message, Exception? innerException)
        : base(message, innerException)
    {
    }

    public override HttpStatusCode StatusCode => HttpStatusCode.Unauthorized;
}
