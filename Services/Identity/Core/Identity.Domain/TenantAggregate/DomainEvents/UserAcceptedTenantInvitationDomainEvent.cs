using BuildingBlocks.Domain.Events;
using Identity.Domain.TenantAggregate.Entities;

namespace Identity.Domain.TenantAggregate.DomainEvents;

public class UserAcceptedTenantInvitationDomainEvent : IDomainEvent
{
    public TenantInvitation Invitation { get; set; }
    
    public UserAcceptedTenantInvitationDomainEvent(TenantInvitation invitation)
    {
        Invitation = invitation;
    }
}