using Application.Dtos.Notifications.Models;
using BuildingBlocks.Application.Schedulers;
using BuildingBlocks.EventBus.Abstracts;
using BusinessFlow.Domain.BusinessFlowAggregate.Repositories;
using BusinessFlow.Domain.SpaceAggregate.Repositories;
using BusinessFlow.Domain.SubmissionExecutionAggregate.Entities;
using BusinessFlow.Domain.SubmissionExecutionAggregate.Repositories;
using Domain.Enums;
using IntegrationEvents.BusinessFlow;
using IntegrationEvents.Hub;

namespace BusinessFlow.Application.UseCases.BusinessFlows.Jobs;

public class SubmissionExecutionPublishIntegrationEventJobHandler : IBackGroundJobHandler<SubmissionExecutionPublishIntegrationEventJob>
{
    private readonly ISpaceRepository _spaceRepository;
    private readonly IBusinessFlowBlockRepository _businessFlowBlockRepository;
    private readonly ISubmissionExecutionRepository _submissionExecutionRepository;
    private readonly IEventPublisher _eventPublisher;
    
    public SubmissionExecutionPublishIntegrationEventJobHandler(ISpaceRepository spaceRepository
        , IBusinessFlowBlockRepository businessFlowBlockRepository
        , ISubmissionExecutionRepository submissionExecutionRepository
        , IEventPublisher eventPublisher)
    {
        _spaceRepository = spaceRepository;
        _businessFlowBlockRepository = businessFlowBlockRepository;
        _submissionExecutionRepository = submissionExecutionRepository;
        _eventPublisher = eventPublisher;
    }
    
    public async Task Handle(SubmissionExecutionPublishIntegrationEventJob notification, CancellationToken cancellationToken)
    {
        var submissionExecution = await _submissionExecutionRepository.FindByIdAsync(notification.SubmissionExecutionId, nameof(SubmissionExecution.PersonInCharges));
        if (submissionExecution == null)
        {
            return;
        }
        
        await PublishIntegrationEventAsync(submissionExecution);
        await PublishNotificationAsync(submissionExecution);
    }
    
    private async Task PublishIntegrationEventAsync(SubmissionExecution submissionExecution)
    {
        var isHasForm = await _businessFlowBlockRepository.IsHasFormAsync(submissionExecution.BusinessFlowBlockId);
        if (!isHasForm)
        {
            return;
        }
        
        await _eventPublisher.Publish(new ExecutionSubmissionCreatedIntegrationEvent(submissionExecution.Id
            , submissionExecution.BusinessFlowBlockId));
    }
    
    private async Task PublishNotificationAsync(SubmissionExecution submissionExecution)
    {
        var space = await _spaceRepository.GetByBlockAsync(submissionExecution.BusinessFlowBlockId);
        if (space == null)
        {
            return;
        }
        
        var personInCharges = submissionExecution.PersonInCharges.Select(x => x.UserId).ToList();
        if (!personInCharges.Any())
        {
            return;
        }
        
        var integrationEvent = new NotificationIntegrationEvent(NotificationType.PersonInChargeAssigned
            , new NotificationPersonInChargeAssignedModel
            {
                SpaceId = space.Id,
                BusinessFlowBlockId = submissionExecution.BusinessFlowBlockId,
                SubmissionId = submissionExecution.SubmissionId
            }
            , personInCharges);

        await _eventPublisher.Publish(integrationEvent);
    }
}