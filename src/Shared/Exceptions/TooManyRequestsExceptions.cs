using System.Net;

namespace Shared.Exceptions;

public class TooManyRequestsExceptions : RestException
{
    public TooManyRequestsExceptions()
        : base()
    {
    }

    public TooManyRequestsExceptions(string message)
        : base(message)
    {
    }

    public TooManyRequestsExceptions(string message, Exception? innerException)
        : base(message, innerException)
    {
    }


    public override HttpStatusCode StatusCode => HttpStatusCode.TooManyRequests;
}
