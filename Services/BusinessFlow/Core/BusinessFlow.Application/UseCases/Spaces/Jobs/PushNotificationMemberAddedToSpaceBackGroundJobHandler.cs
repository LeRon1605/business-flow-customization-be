using Application.Dtos.Notifications.Models;
using BuildingBlocks.Application.Schedulers;
using BuildingBlocks.EventBus.Abstracts;
using BusinessFlow.Domain.SpaceAggregate.Repositories;
using Domain.Enums;
using IntegrationEvents.Hub;

namespace BusinessFlow.Application.UseCases.Spaces.Jobs;

public class PushNotificationMemberAddedToSpaceBackGroundJobHandler : IBackGroundJobHandler<PushNotificationMemberAddedToSpaceBackGroundJob>
{
    private readonly ISpaceRepository _spaceRepository;
    private readonly IEventPublisher _eventPublisher;
    
    public PushNotificationMemberAddedToSpaceBackGroundJobHandler(ISpaceRepository spaceRepository
        , IEventPublisher eventPublisher)
    {
        _spaceRepository = spaceRepository;
        _eventPublisher = eventPublisher;
    }
    
    public async Task Handle(PushNotificationMemberAddedToSpaceBackGroundJob notification, CancellationToken cancellationToken)
    {
        var space = await _spaceRepository.FindByIdAsync(notification.SpaceId);
        if (space == null || space.CreatedBy == notification.MemberId)
            return;

        await _eventPublisher.Publish(new NotificationIntegrationEvent(NotificationType.MemberAddedToSpace
            , new NotificationMemberAddedToSpaceModel()
            {
                SpaceId = notification.SpaceId,
                SpaceName = space.Name,
                MemberId = notification.MemberId,
            }
            , new List<string>() { notification.MemberId }), cancellationToken);
    }
}