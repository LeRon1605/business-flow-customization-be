using BuildingBlocks.Domain.Exceptions.Resources;

namespace Identity.Domain.UserAggregate.Exceptions;

public class RefreshTokenNotFoundException : ResourceNotFoundException
{
    public RefreshTokenNotFoundException(string refreshToken) 
        : base($"Refresh token '{refreshToken}' not found!", ErrorCodes.RefreshTokenNotFound)
    {
    }
}