using BuildingBlocks.Domain.Exceptions.Resources;

namespace Identity.Domain.RoleAggregate.Exceptions;

public class PermissionHasAlreadyGrantedToRoleException : ResourceAlreadyExistException
{
    public PermissionHasAlreadyGrantedToRoleException(string roleId, int permissionId) 
        : base($"Permission with id '{permissionId}' has already been granted to role with id '{roleId}'!", ErrorCodes.PermissionHasAlreadyGrantedToRole)
    {
    }
}