namespace BuildingBlocks.Domain.Exceptions.Resources;

public class ResourceUnauthorizedAccessException : CoreException
{
    public ResourceUnauthorizedAccessException(string message) : base(message, ErrorCodes.ResourceUnauthorizedAccess)
    {
    }

    public ResourceUnauthorizedAccessException(string message, string errorCode) : base(message, errorCode)
    {
    }
}