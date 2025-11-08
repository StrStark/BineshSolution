using System.Net;

namespace BineshSoloution.Exceptions;

public class ConflictException : RestException
{
    public ConflictException()
        : this("Request could not be processed because of conflict in the request")
    {
    }

    public ConflictException(string message)
        : base(message)
    {
    }

    public ConflictException(string message, Exception? innerException)
        : base(message, innerException)
    {
    }


    public override HttpStatusCode StatusCode => HttpStatusCode.Conflict;
}
