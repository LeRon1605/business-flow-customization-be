using BuildingBlocks.Domain.Exceptions.Resources;

namespace Identity.Domain.UserAggregate.Exceptions;

public class UserAlreadyInTenantException : ResourceAlreadyExistException
{
    public UserAlreadyInTenantException(string userId, int tenantId) 
        : base($"User with id {userId} is already in tenant with id {tenantId}.", ErrorCodes.UserAlreadyInTenant)
    {
    }
}