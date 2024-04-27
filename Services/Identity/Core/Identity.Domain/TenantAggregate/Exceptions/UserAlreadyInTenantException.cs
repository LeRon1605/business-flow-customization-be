using BuildingBlocks.Domain.Exceptions.Resources;

namespace Identity.Domain.TenantAggregate.Exceptions;

public class UserAlreadyInTenantException : ResourceAlreadyExistException
{
    public UserAlreadyInTenantException(string email) : base($"{email} has already in tenant", ErrorCodes.UserAlreadyInTenant)
    {
    }
}