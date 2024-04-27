using BuildingBlocks.Domain.Models;
using Identity.Domain.RoleAggregate.Entities;
using Identity.Domain.TenantAggregate.DomainEvents;
using Identity.Domain.TenantAggregate.Enums;

namespace Identity.Domain.TenantAggregate.Entities;

public class TenantInvitation : AuditableEntity
{
    public string Email { get; set; }
    public string Token { get; set; }
    public int TenantId { get; set; }
    public string RoleId { get; set; }
    public TenantInvitationStatus Status { get; set; }
    
    public Tenant Tenant { get; set; }
    public ApplicationRole Role { get; set; }
    
    public TenantInvitation(string email, int tenantId, string roleId)
    {
        Email = email;
        TenantId = tenantId;
        RoleId = roleId;
        Status = TenantInvitationStatus.Pending;
        Token = Guid.NewGuid().ToString();
        
        AddDomainEvent(new UserInvitedToTenantDomainEvent(this));
    }
    
    public void Accept()
    {
        Status = TenantInvitationStatus.Accepted;
        
        AddDomainEvent(new UserAcceptedTenantInvitationDomainEvent(this));
    }
    
    private TenantInvitation()
    {
        
    }
}