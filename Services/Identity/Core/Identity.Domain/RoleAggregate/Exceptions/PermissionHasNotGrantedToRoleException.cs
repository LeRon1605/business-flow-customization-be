using BuildingBlocks.Domain.Exceptions.Resources;

namespace Identity.Domain.RoleAggregate.Exceptions;

public class PermissionHasNotGrantedToRoleException : ResourceNotFoundException
{
    public PermissionHasNotGrantedToRoleException(string roleId, int permissionId) 
        : base($"Permission with id '{permissionId}' has not been granted to role with id '{roleId}' yet!", ErrorCodes.PermissionHasNotGrantedToRole)
    {
    }
}