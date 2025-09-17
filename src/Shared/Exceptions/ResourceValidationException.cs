using System.Net;

namespace Shared.Exceptions;

public class ResourceValidationException : RestException
{
    public ResourceValidationException(params string[] errorMessages)
    : this([("*", errorMessages)])
    {

    }

    public ResourceValidationException((string propName, string[] errorMessages)[] details)
        : this("*", details)
    {

    }

    public ResourceValidationException(Type resourceType, (string propName, string[] errorMessages)[] details)
        : this(resourceType.FullName!, details)
    {

    }

    public ResourceValidationException(string resourceTypeName, (string propName, string[] errorMessages)[] details)
        : this(new ErrorResourcePayload()
        {
            ResourceTypeName = resourceTypeName,
            Details = details.Select(propErrors => new PropertyErrorResourceCollection
            {
                Name = propErrors.propName,
                Errors = propErrors.errorMessages.Select(e => new ErrorResource()
                {
                    Key = e, // i dont really know what im doing :(
                    Message = e
                }).ToList()
            }).ToList()
        })
    {

    }

    public ResourceValidationException(ErrorResourcePayload payload)
        : this(message: "Request data is not valid", payload)
    {

    }

    public ResourceValidationException(string message, ErrorResourcePayload payload)
        : base(message)
    {
        Payload = payload;
    }

    public ErrorResourcePayload Payload { get; set; } = new ErrorResourcePayload();

    public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
}
