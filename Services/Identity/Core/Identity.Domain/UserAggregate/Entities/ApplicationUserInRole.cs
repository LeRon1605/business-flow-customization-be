using Identity.Domain.TenantAggregate.Entities;
using Microsoft.AspNetCore.Identity;
using ApplicationRole = Identity.Domain.RoleAggregate.Entities.ApplicationRole;

namespace Identity.Domain.UserAggregate.Entities;

public class ApplicationUserInRole : IdentityUserRole<string>
{
    public int TenantId { get; set; }
    public Tenant? Tenant { get; set; }
    
    public ApplicationRole? Role { get; set; }
    
    public ApplicationUserInRole(string userId, string roleId, int tenantId)
    {
        UserId = userId;
        RoleId = roleId;
        TenantId = tenantId;
    }
    
    public ApplicationUserInRole()
    {
    }
}