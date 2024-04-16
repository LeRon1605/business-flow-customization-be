namespace BuildingBlocks.Domain.Exceptions.Resources;

public class ResourceInvalidDataException : CoreException
{
    public ResourceInvalidDataException(string message) : base(message, ErrorCodes.ResourceInvalidData)
    {
    }
    
    public ResourceInvalidDataException(string message, string code) : base(message, code)
    {
    }
}