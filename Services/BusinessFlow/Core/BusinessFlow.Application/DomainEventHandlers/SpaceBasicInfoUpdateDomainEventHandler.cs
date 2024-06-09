using BuildingBlocks.Domain.Events;
using BusinessFlow.Domain.BusinessFlowAggregate.DomainEvents;

namespace BusinessFlow.Application.DomainEventHandlers;

public class SpaceBasicInfoUpdateDomainEventHandler : IDomainEventHandler<SpaceBasicInfoUpdateDomainEvent>
{
    public Task Handle(SpaceBasicInfoUpdateDomainEvent notification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}