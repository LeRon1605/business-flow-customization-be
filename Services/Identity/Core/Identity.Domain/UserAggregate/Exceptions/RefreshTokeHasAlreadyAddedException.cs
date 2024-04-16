using BuildingBlocks.Domain.Exceptions.Resources;

namespace Identity.Domain.UserAggregate.Exceptions;

public class RefreshTokeHasAlreadyAddedException : ResourceAlreadyExistException
{
    public RefreshTokeHasAlreadyAddedException(string refreshToken) 
        : base($"Refresh token '{refreshToken}' for access token has already added!", ErrorCodes.RefreshTokenAlreadyAdded)
    {
    }
}