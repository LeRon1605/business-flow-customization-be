using BuildingBlocks.Domain.Exceptions.Resources;

namespace Identity.Domain.UserAggregate.Exceptions;

public class UserNotInTenantException : ResourceNotFoundException
{
    public UserNotInTenantException(string userId, int tenantId) 
        : base($"User with id {userId} is not in tenant with id {tenantId}.", ErrorCodes.UserNotInTenant)
    {
    }
}