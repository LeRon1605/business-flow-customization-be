using BuildingBlocks.Domain.Events;

namespace BuildingBlocks.Domain.Models.Interfaces;

public interface IDomainModel
{
    IReadOnlyCollection<IDomainEvent>? DomainEvents { get; }

    void AddDomainEvent(IDomainEvent eventItem);

    void RemoveDomainEvent(IDomainEvent eventItem);

    void ClearDomainEvents();
}