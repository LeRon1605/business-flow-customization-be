using BuildingBlocks.Application.Schedulers;
using BuildingBlocks.Domain.Events;
using Hub.Application.UseCases.Comments.Jobs;
using Hub.Domain.CommentAggregate.DomainEvents;

namespace Hub.Application.DomainEventHandlers;

public class CommentCreatedDomainEventHandler : IDomainEventHandler<CommentCreatedDomainEvent>
{
    private readonly IBackGroundJobManager _backGroundJobManager;
    
    public CommentCreatedDomainEventHandler(IBackGroundJobManager backGroundJobManager)
    {
        _backGroundJobManager = backGroundJobManager;
    }
    
    public async Task Handle(CommentCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        _backGroundJobManager.Fire(new PushCommentNotificationBackGroundJob(notification.Comment.Id));
    }
}