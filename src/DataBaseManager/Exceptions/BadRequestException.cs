using System.Net;

namespace BineshSoloution.Exceptions;

public class BadRequestException : RestException
{
    public BadRequestException() : base() { }
    public BadRequestException(string message)
        : base(message)
    {
    }

    public BadRequestException(string message, Exception? innerException)
        : base(message, innerException)
    {
    }

    public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
}
