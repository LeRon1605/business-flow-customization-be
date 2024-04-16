using BuildingBlocks.Domain.Exceptions.Resources;

namespace Identity.Domain.UserAggregate.Exceptions;

public class AccessTokenStillValidException : ResourceInvalidOperationException
{
    public AccessTokenStillValidException() 
        : base("Access token still valid!", ErrorCodes.AccessTokenStillValid)
    {
    }
}