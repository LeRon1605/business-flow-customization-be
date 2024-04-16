using BuildingBlocks.Domain.Models;

namespace Identity.Domain.RoleAggregate.Entities;

public record RolePermission : ValueObject
{
    public string RoleId { get; set; }
    public int PermissionId { get; set; }

    public RolePermission(string roleId, int permissionId)
    {
        RoleId = Guard.NotNullOrEmpty(roleId, nameof(RoleId));
        PermissionId = Guard.NotNull(permissionId, nameof(PermissionId));
    }
}