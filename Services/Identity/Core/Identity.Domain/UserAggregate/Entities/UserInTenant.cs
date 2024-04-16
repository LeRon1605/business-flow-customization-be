using BuildingBlocks.Domain.Models;
using Identity.Domain.TenantAggregate.Entities;

namespace Identity.Domain.UserAggregate.Entities;

public record UserInTenant : ValueObject
{
    public int TenantId { get; set; }
    public string UserId { get; set; }
    
    public bool IsOwner { get; set; }
    
    public ApplicationUser? User { get; set; }
    public Tenant? Tenant { get; set; }
    
    public UserInTenant(int tenantId, string userId, bool isOwner)
    {
        TenantId = tenantId;
        UserId = userId;
        IsOwner = isOwner;
    }

    private UserInTenant()
    {
    }
}