﻿namespace BuildingBlocks.Domain.Exceptions.Resources;

public class ResourceInvalidOperationException : CoreException
{
    public ResourceInvalidOperationException(string message) : base(message, ErrorCodes.ResourceInvalidOperation)
    {
    }

    public ResourceInvalidOperationException(string message, string errorCode) : base(message, errorCode)
    {
    }
}