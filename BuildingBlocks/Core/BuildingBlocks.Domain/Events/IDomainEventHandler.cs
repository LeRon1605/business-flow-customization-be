using MediatR;

namespace BuildingBlocks.Domain.Events;

public interface IDomainEventHandler<in TEvent> : INotificationHandler<TEvent> where TEvent : IDomainEvent
{
}