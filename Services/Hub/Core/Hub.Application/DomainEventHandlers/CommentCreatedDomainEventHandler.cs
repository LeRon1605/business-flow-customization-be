using BuildingBlocks.Application.Identity;
using BuildingBlocks.Domain.Events;
using Domain.Enums;
using Hub.Application.Services.Abstracts;
using Hub.Domain.CommentAggregate.DomainEvents;
using Hub.Domain.CommentAggregate.Enums;

namespace Hub.Application.DomainEventHandlers;

public class CommentCreatedDomainEventHandler : IDomainEventHandler<CommentCreatedDomainEvent>
{
    private readonly INotificationSenderService _notificationSenderService;
    private readonly ICurrentUser _currentUser;
    
    public CommentCreatedDomainEventHandler(INotificationSenderService notificationSenderService
        , ICurrentUser currentUser)
    {
        _notificationSenderService = notificationSenderService;
        _currentUser = currentUser;
    }
    
    public async Task Handle(CommentCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        var mentionUserIds = notification.Comment.Mentions
            .Where(x => x.EntityType == MentionEntity.User)
            .SelectMany(x => x.EntityIds)
            .ToList();
        
        await _notificationSenderService.SendAsync(mentionUserIds
            , _currentUser.TenantId
            , string.Empty
            , NotificationType.SubmissionCommentMentioned);
    }
}