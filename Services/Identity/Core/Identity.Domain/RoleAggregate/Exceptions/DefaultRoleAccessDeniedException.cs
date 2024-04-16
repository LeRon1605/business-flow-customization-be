using BuildingBlocks.Domain.Exceptions.Resources;

namespace Identity.Domain.RoleAggregate.Exceptions;

public class DefaultRoleAccessDeniedException : ResourceInvalidOperationException
{
    public DefaultRoleAccessDeniedException(string role) 
        : base($"Can not create, update or delete '{role}' role!", ErrorCodes.DefaultRoleAccessDenied)
    {
    }
}