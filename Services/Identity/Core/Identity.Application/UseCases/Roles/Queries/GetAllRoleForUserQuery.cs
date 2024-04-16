using BuildingBlocks.Application.Cqrs;

namespace Identity.Application.UseCases.Roles.Queries;

public class GetAllRoleForUserQuery : IQuery<IEnumerable<string>>
{
    public string UserId { get; set; }
    public int TenantId { get; set; }
    
    public GetAllRoleForUserQuery(string userId, int tenantId)
    {
        UserId = userId;
        TenantId = tenantId;
    }
}