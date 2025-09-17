using System.Net;

namespace Shared.Exceptions;

public class ResourceNotFoundException : RestException
{
    public ResourceNotFoundException()
        : base("Resource not found")
    {
    }

    public ResourceNotFoundException(string message)
        : base(message)
    {
    }

    public ResourceNotFoundException(string message, Exception? innerException)
        : base(message, innerException)
    {
    

    }

    public override HttpStatusCode StatusCode => HttpStatusCode.NotFound;
}
