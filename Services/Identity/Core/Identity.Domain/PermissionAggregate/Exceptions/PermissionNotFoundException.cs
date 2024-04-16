using BuildingBlocks.Domain.Exceptions.Resources;
using ServeSync.Infrastructure.Identity.Models.PermissionAggregate;

namespace Identity.Domain.PermissionAggregate.Exceptions;

public class PermissionNotFoundException : ResourceNotFoundException
{
    public PermissionNotFoundException(int id) : base("Permission", id, ErrorCodes.PermissionNotFound)
    {
    }
}