using Application.Dtos;
using Application.Dtos.Identity;
using BuildingBlocks.Application.Cqrs;

namespace Identity.Application.UseCases.Permissions.Queries;

public class GetAllPermissionForRoleQuery : IQuery<IEnumerable<PermissionDto>>
{
    public string RoleId { get; set; }
    public string Name { get; set; }
    
    public GetAllPermissionForRoleQuery(string roleId, string name)
    {
        RoleId = roleId;
        Name = name;
    }
}