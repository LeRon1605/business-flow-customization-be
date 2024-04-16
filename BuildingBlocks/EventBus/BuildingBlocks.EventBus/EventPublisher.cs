using BuildingBlocks.EventBus.Abstracts;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.EventBus;

public class EventPublisher : IEventPublisher
{
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly ILogger<EventPublisher> _logger;
    
    public EventPublisher(IPublishEndpoint publishEndpoint, ILogger<EventPublisher> logger)
    {
        _publishEndpoint = publishEndpoint;
        _logger = logger;
    }

    public Task Publish(object message, CancellationToken cancellationToken = default)
    {
        return _publishEndpoint.Publish(message, cancellationToken);
    }

    public Task Publish<T>(T message, CancellationToken cancellationToken = default) where T : IIntegrationEvent
    {
        _logger.LogInformation("Published event {Event}", nameof(T));
        return _publishEndpoint.Publish(message, cancellationToken);
    }
}