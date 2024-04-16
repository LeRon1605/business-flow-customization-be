using MassTransit;

namespace BuildingBlocks.EventBus.Abstracts;

public interface IIntegrationEventHandler
{
    
}

public interface IIntegrationEventHandler<in TEvent> : IIntegrationEventHandler, IConsumer<TEvent> where TEvent : class, IIntegrationEvent
{
    
}