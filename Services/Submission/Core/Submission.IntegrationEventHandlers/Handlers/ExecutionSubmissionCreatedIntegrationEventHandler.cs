using Application.Dtos.Notifications.Models;
using BuildingBlocks.Application.Data;
using BuildingBlocks.Application.Identity;
using BuildingBlocks.EventBus;
using BuildingBlocks.EventBus.Abstracts;
using Domain.Enums;
using IntegrationEvents.BusinessFlow;
using IntegrationEvents.Hub;
using Microsoft.Extensions.Logging;
using Submission.Domain.FormAggregate.Exceptions;
using Submission.Domain.FormAggregate.Repositories;
using Submission.Domain.SubmissionAggregate.DomainServices.Abstracts;
using Submission.Domain.SubmissionAggregate.Models;

namespace Submission.IntegrationEvents.Handlers;

public class ExecutionSubmissionCreatedIntegrationEventHandler : IntegrationEventHandler<ExecutionSubmissionCreatedIntegrationEvent>
{
    private readonly IFormRepository _formRepository;
    private readonly ISubmissionDomainService _submissionDomainService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEventPublisher _eventPublisher;
    
    public ExecutionSubmissionCreatedIntegrationEventHandler(ICurrentUser currentUser
        , ILogger<IntegrationEventHandler<ExecutionSubmissionCreatedIntegrationEvent>> logger
        , IFormRepository formRepository
        , ISubmissionDomainService submissionDomainService
        , IUnitOfWork unitOfWork
        , IEventPublisher eventPublisher) : base(currentUser, logger)
    {
        _formRepository = formRepository;
        _submissionDomainService = submissionDomainService;
        _unitOfWork = unitOfWork;
        _eventPublisher = eventPublisher;
    }

    public override async Task HandleAsync(ExecutionSubmissionCreatedIntegrationEvent @event)
    {
        var form = await _formRepository.FindByBusinessFlowBlockIdAsync(@event.BusinessFlowBlockId);
        if (form == null)
        {
            throw new BusinessFlowBlockFormNotFoundException(@event.BusinessFlowBlockId);
        }

        var submissionModel = new SubmissionModel()
        {
            Name = form.Name,
            FormVersionId = form.Versions.Last().Id,
            Fields = new List<SubmissionFieldModel>()
        };
        
        var formSubmission = await _submissionDomainService.CreateAsync(form.SpaceId, @event.ExecutionId, submissionModel);
        await _unitOfWork.CommitAsync();

        await _eventPublisher.Publish(new RealTimeIntegrationEvent(NotificationType.SubmissionExecutionInitiated,
            new NotificationSubmissionExecutionInitiatedModel()
            {
                Id = formSubmission.Id,
                ExecutionId = formSubmission.ExecutionId!.Value
            }, new List<string> { CurrentUser.Id }));
    }
}