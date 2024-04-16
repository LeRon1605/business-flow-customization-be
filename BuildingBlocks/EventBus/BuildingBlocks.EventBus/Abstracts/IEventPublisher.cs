using BuildingBlocks.Kernel.Services;

namespace BuildingBlocks.EventBus.Abstracts;

public interface IEventPublisher : IScopedService
{
    Task Publish(object message, CancellationToken cancellationToken = default);
    
    Task Publish<T>(T message, CancellationToken cancellationToken = default)
        where T : IIntegrationEvent;
}