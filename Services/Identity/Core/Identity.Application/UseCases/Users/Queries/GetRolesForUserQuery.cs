using BuildingBlocks.Application.Cqrs;
using Identity.Application.UseCases.Roles.Dtos;

namespace Identity.Application.UseCases.Users.Queries;

public class GetRolesForUserQuery : IQuery<List<RoleDto>>
{
    public string UserId { get; set; }
    public int TenantId { get; set; }
    
    public GetRolesForUserQuery(string userId, int tenantId)
    {
        UserId = userId;
        TenantId = tenantId;
    }
}