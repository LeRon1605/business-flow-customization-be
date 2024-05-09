using BuildingBlocks.Application.Identity;
using BuildingBlocks.EventBus.Abstracts;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.EventBus;

public class EventPublisher : IEventPublisher
{
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly ICurrentUser _currentUser;
    private readonly ILogger<EventPublisher> _logger;
    
    public EventPublisher(IPublishEndpoint publishEndpoint, ICurrentUser currentUser, ILogger<EventPublisher> logger)
    {
        _publishEndpoint = publishEndpoint;
        _currentUser = currentUser;
        _logger = logger;
    }

    public Task Publish(object message, CancellationToken cancellationToken = default)
    {
        return _publishEndpoint.Publish(message, cancellationToken);
    }

    public Task Publish<T>(T message, CancellationToken cancellationToken = default) where T : IIntegrationEvent
    {
        if (_currentUser.IsAuthenticated)
        {
            message.UserId = _currentUser.Id;
            message.TenantId = _currentUser.TenantId;
        }
        
        _logger.LogInformation("Published event {Event}", typeof(T).Name);
        return _publishEndpoint.Publish(message, cancellationToken);
    }
}