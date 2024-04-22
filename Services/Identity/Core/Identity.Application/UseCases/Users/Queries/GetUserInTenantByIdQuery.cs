using BuildingBlocks.Application.Cqrs;
using Identity.Application.UseCases.Users.Dtos;

namespace Identity.Application.UseCases.Users.Queries;

public class GetUserInTenantByIdQuery : IQuery<UserDetailDto>
{
    public int TenantId { get; set; }
    public string UserId { get; set; }
    
    public GetUserInTenantByIdQuery(int tenantId, string userId)
    {
        TenantId = tenantId;
        UserId = userId;
    }
}