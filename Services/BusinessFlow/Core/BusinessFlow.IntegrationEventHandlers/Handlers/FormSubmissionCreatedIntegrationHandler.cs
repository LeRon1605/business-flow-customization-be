using BuildingBlocks.Application.Data;
using BuildingBlocks.Application.Identity;
using BuildingBlocks.EventBus;
using BusinessFlow.Domain.SubmissionExecutionAggregate.DomainServices;
using IntegrationEvents.FormSubmissions;
using Microsoft.Extensions.Logging;

namespace BusinessFlow.IntegrationEventHandler.Handlers;

public class FormSubmissionCreatedIntegrationHandler : IntegrationEventHandler<FormSubmissionCreatedIntegrationEvent>
{
    private readonly IBusinessFlowExecutorDomainService _businessFlowExecutorDomainService;
    private readonly IUnitOfWork _unitOfWork;
    
    public FormSubmissionCreatedIntegrationHandler(ICurrentUser currentUser
        , ILogger<IntegrationEventHandler<FormSubmissionCreatedIntegrationEvent>> logger
        , IBusinessFlowExecutorDomainService businessFlowExecutorDomainService
        , IUnitOfWork unitOfWork) : base(currentUser, logger)
    {
        _businessFlowExecutorDomainService = businessFlowExecutorDomainService;
        _unitOfWork = unitOfWork;
    }

    public override async Task HandleAsync(FormSubmissionCreatedIntegrationEvent @event)
    {
        await _businessFlowExecutorDomainService.StartAsync(@event.BusinessFlowVersionId, @event.SubmissionId);
        
        await _unitOfWork.CommitAsync();
    }
}