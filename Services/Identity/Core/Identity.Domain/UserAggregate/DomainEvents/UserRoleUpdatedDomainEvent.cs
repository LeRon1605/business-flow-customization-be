using ServeSync.Domain.SeedWorks.Events;

namespace Identity.Domain.UserAggregate.DomainEvents;

public record UserRoleUpdatedDomainEvent : EquatableDomainEvent
{
    public string UserId { get; set; }
    public int TenantId { get; set; }

    public UserRoleUpdatedDomainEvent(string userId, int tenantId)
    {
        UserId = userId;
        TenantId = tenantId;
    }
}