using BuildingBlocks.Domain.Events;
using BuildingBlocks.EventBus.Abstracts;
using Identity.Domain.TenantAggregate.DomainEvents;

namespace Identity.Application.DomainEventHandlers;

public class RemoveUserInTenantDomainEventHandler : IDomainEventHandler<RemoveUserInTenantDomainEvent>
{
    private readonly IEventPublisher _eventPublisher;
    
    public RemoveUserInTenantDomainEventHandler(IEventPublisher eventPublisher)
    {
        _eventPublisher = eventPublisher;
    }

    public Task Handle(RemoveUserInTenantDomainEvent notification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}