using ServeSync.Domain.SeedWorks.Events;

namespace Identity.Domain.RoleAggregate.DomainEvents;

public record PermissionForRoleUpdatedDomainEvent : EquatableDomainEvent
{
    public string Name { get; }
    
    public PermissionForRoleUpdatedDomainEvent(string name)
    {
        Name = name;
    }
}