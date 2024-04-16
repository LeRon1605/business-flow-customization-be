using Application.Dtos;
using BuildingBlocks.Application.Cqrs;

namespace Identity.Application.UseCases.Permissions.Commands;

public class UpdatePermissionForRoleCommand : ICommand<IEnumerable<PermissionDto>>
{
    public string RoleId { get; set; }
    public IEnumerable<int> PermissionIds { get; set; }
    
    public UpdatePermissionForRoleCommand(string roleId, IEnumerable<int> permissionIds)
    {
        RoleId = roleId;
        PermissionIds = permissionIds;
    }
}