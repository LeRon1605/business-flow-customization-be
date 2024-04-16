using ServeSync.Domain.SeedWorks.Events;

namespace Identity.Domain.RoleAggregate.DomainEvents;

public record RoleNameUpdatedDomainEvent : EquatableDomainEvent
{
    public string Name { get; }
    
    public RoleNameUpdatedDomainEvent(string name)
    {
        Name = name;
    }
}