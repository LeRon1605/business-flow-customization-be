using BuildingBlocks.Application.Data;
using BuildingBlocks.Application.Identity;
using BuildingBlocks.EventBus;
using IntegrationEvents.BusinessFlow;
using Microsoft.Extensions.Logging;
using Submission.Domain.SpaceBusinessFlowAggregate.DomainServices;

namespace Submission.IntegrationEvents.Handlers;

public class SpaceBusinessFlowVersionCreatedIntegrationEventHandler : IntegrationEventHandler<SpaceBusinessFlowVersionCreatedIntegrationEvent>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISpaceBusinessFlowDomainService _spaceBusinessFlowDomainService;
    
    public SpaceBusinessFlowVersionCreatedIntegrationEventHandler(ICurrentUser currentUser
        , ILogger<IntegrationEventHandler<SpaceBusinessFlowVersionCreatedIntegrationEvent>> logger
        , IUnitOfWork unitOfWork
        , ISpaceBusinessFlowDomainService spaceBusinessFlowDomainService) : base(currentUser, logger)
    {
        _unitOfWork = unitOfWork;
        _spaceBusinessFlowDomainService = spaceBusinessFlowDomainService;
    }
    
    public override async Task HandleAsync(SpaceBusinessFlowVersionCreatedIntegrationEvent @event)
    {
        var spaceBusinessFlow = await _spaceBusinessFlowDomainService.AddOrUpdateVersionAsync(@event.SpaceId, @event.BusinessFlowVersionId);
        
        await _unitOfWork.CommitAsync();
    }
}