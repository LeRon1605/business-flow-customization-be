using BuildingBlocks.Domain.Events;
using BuildingBlocks.EventBus.Abstracts;
using BusinessFlow.Domain.BusinessFlowAggregate.DomainEvents;
using IntegrationEvents.BusinessFlow;

namespace BusinessFlow.Application.DomainEventHandlers;

public class BusinessFlowVersionCreatedDomainEventHandler : IPersistedDomainEventHandler<BusinessFlowVersionCreatedDomainEvent>
{
    private readonly IEventPublisher _eventPublisher;
    
    public BusinessFlowVersionCreatedDomainEventHandler(IEventPublisher eventPublisher)
    {
        _eventPublisher = eventPublisher;
    }
    
    public async Task Handle(BusinessFlowVersionCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        var businessFlowVersion = notification.BusinessFlowVersion;
        var integrationEvent = new SpaceBusinessFlowVersionCreatedIntegrationEvent(businessFlowVersion.SpaceId, businessFlowVersion.Id);
        
        await _eventPublisher.Publish(integrationEvent, cancellationToken);
    }
}