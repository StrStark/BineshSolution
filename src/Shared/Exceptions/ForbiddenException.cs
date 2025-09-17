﻿using System.Net;

namespace Shared.Exceptions;

public class ForbiddenException : RestException
{
    public ForbiddenException()
        : base("Access to the requested resource is forbidden")
    {
    }

    public ForbiddenException(string message)
        : base(message)
    {
    }

    public ForbiddenException(string message, Exception? innerException)
        : base(message, innerException)
    {
    }

    public override HttpStatusCode StatusCode => HttpStatusCode.Forbidden;
}
