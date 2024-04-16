using BuildingBlocks.Domain.Exceptions.Resources;

namespace Identity.Domain.UserAggregate.Exceptions;

public class RefreshTokenHasAlreadyExpireException : ResourceInvalidOperationException
{
    public RefreshTokenHasAlreadyExpireException(string refreshToken) 
        : base($"Refresh token '{refreshToken}' has already expired!", ErrorCodes.RefreshTokenAlreadyExpire)
    {
    }
}