using Application.Dtos;
using BuildingBlocks.Application.Cqrs;

namespace Identity.Application.UseCases.Permissions.Queries;

public class GetAllPermissionForUserQuery : IQuery<IEnumerable<PermissionDto>>
{
    public string UserId { get; set; }
    public int TenantId { get; set; }

    public GetAllPermissionForUserQuery(string userId, int tenantId)
    {
        UserId = userId;
        TenantId = tenantId;
    }
}