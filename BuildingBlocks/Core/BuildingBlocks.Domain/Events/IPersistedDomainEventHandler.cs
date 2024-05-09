namespace BuildingBlocks.Domain.Events;

public interface IPersistedDomainEventHandler
{
    
}

public interface IPersistedDomainEventHandler<in TEvent> : IPersistedDomainEventHandler where TEvent : IDomainEvent
{
    Task Handle(TEvent @event, CancellationToken cancellationToken);
}