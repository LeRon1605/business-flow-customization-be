using BuildingBlocks.Domain.Events;

namespace Identity.Domain.TenantAggregate.DomainEvents;

public class RemoveUserInTenantDomainEvent: IDomainEvent
{
    public string UserId { get; set; }
    public RemoveUserInTenantDomainEvent(string userId)
    {
        UserId = userId;
    }
}