using BuildingBlocks.Application.Schedulers;
using BuildingBlocks.Domain.Events;
using BusinessFlow.Application.UseCases.Spaces.Jobs;
using BusinessFlow.Domain.SpaceAggregate.DomainEvents;

namespace BusinessFlow.Application.DomainEventHandlers;

public class MemberAddedToSpaceDomainEventHandler : IDomainEventHandler<MemberAddedToSpaceDomainEvent>
{
    private readonly IBackGroundJobManager _backGroundJobManager;
    
    public MemberAddedToSpaceDomainEventHandler(IBackGroundJobManager backGroundJobManager)
    {
        _backGroundJobManager = backGroundJobManager;
    }
    
    public Task Handle(MemberAddedToSpaceDomainEvent notification, CancellationToken cancellationToken)
    {
        _backGroundJobManager.Fire(new PushNotificationMemberAddedToSpaceBackGroundJob(notification.SpaceId, notification.UserId));
        return Task.CompletedTask;
    }
}