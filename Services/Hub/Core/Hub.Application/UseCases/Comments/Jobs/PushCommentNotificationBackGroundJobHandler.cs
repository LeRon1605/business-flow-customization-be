using System.Text.Json;
using Application.Dtos.Notifications.Models;
using BuildingBlocks.Application.Identity;
using BuildingBlocks.Application.Schedulers;
using Domain.Enums;
using Hub.Application.Clients.Abstracts;
using Hub.Application.Services.Abstracts;
using Hub.Domain.CommentAggregate.Enums;
using Hub.Domain.CommentAggregate.Repositories;
using Newtonsoft.Json;

namespace Hub.Application.UseCases.Comments.Jobs;

public class PushCommentNotificationBackGroundJobHandler : IBackGroundJobHandler<PushCommentNotificationBackGroundJob>
{
    private readonly ICurrentUser _currentUser;
    private readonly ICommentRepository _commentRepository;
    private readonly INotificationSenderService _notificationSenderService;
    private readonly IInternalBusinessFlowClient _internalBusinessFlowClient;
    
    public PushCommentNotificationBackGroundJobHandler(ICurrentUser currentUser
        , ICommentRepository commentRepository
        , INotificationSenderService notificationSenderService
        , IInternalBusinessFlowClient internalBusinessFlowClient)
    {
        _currentUser = currentUser;
        _commentRepository = commentRepository;
        _notificationSenderService = notificationSenderService;
        _internalBusinessFlowClient = internalBusinessFlowClient;
    }
    
    public async Task Handle(PushCommentNotificationBackGroundJob notification, CancellationToken cancellationToken)
    {
        var comment = await _commentRepository.FindByIdAsync(notification.CommentId);
        if (comment == null)
            return;
        
        var mentionedUserIds = comment.Mentions
            .Where(x => x.EntityType == MentionEntity.User)
            .SelectMany(x => x.EntityIds)
            .Where(x => !string.IsNullOrEmpty(x) && x != _currentUser.Id)
            .Distinct()
            .ToList();
        if (mentionedUserIds.Any())
            await _notificationSenderService.SendAsync(mentionedUserIds!
                , _currentUser.TenantId
                , JsonConvert.SerializeObject(new NotificationSubmissionCommentModel()
                {
                    Id = comment.Id,
                    SubmissionId = int.Parse(comment.EntityId),
                    Content = comment.Content
                })
                , NotificationType.SubmissionCommentMentioned);
        
        var submissionPersonInCharges = await _internalBusinessFlowClient
            .GetExecutionPersonInChargeDataAsync(new List<int> { int.Parse(comment.EntityId) });
        
        var personInChargeIds = submissionPersonInCharges
            .SelectMany(x => JsonConvert.DeserializeObject<List<string>>(x.Id.ToString()!)!)
            .Where(x => !string.IsNullOrEmpty(x) && x != _currentUser.Id  && !mentionedUserIds.Contains(x))
            .Distinct()
            .ToList();
        
        if (personInChargeIds.Any())
            await _notificationSenderService.SendAsync(personInChargeIds!
                , _currentUser.TenantId
                , JsonConvert.SerializeObject(new NotificationSubmissionCommentModel()
                {
                    Id = comment.Id,
                    SubmissionId = int.Parse(comment.EntityId),
                    Content = comment.Content
                })
                , NotificationType.SubmissionComment);
    }
}