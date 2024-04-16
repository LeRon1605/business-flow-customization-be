using BuildingBlocks.Domain.Exceptions.Resources;
using ApplicationRole = Identity.Domain.RoleAggregate.Entities.ApplicationRole;

namespace Identity.Domain.RoleAggregate.Exceptions;

public class RoleNotFoundException : ResourceNotFoundException
{
    public RoleNotFoundException(string id) : base("Role", nameof(ApplicationRole.Id),id, ErrorCodes.RoleNotFound)
    {
    }
    
    public RoleNotFoundException(string column, string value) : base("Role", column, value, ErrorCodes.RoleNotFound)
    {
    }
}