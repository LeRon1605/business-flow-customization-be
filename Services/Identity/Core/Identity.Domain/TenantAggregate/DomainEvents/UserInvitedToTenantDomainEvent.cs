using BuildingBlocks.Domain.Events;
using Identity.Domain.TenantAggregate.Entities;

namespace Identity.Domain.TenantAggregate.DomainEvents;

public class UserInvitedToTenantDomainEvent : IDomainEvent
{
    public TenantInvitation Invitation { get; set; }
    
    public UserInvitedToTenantDomainEvent(TenantInvitation invitation)
    {
        Invitation = invitation;
    }
}