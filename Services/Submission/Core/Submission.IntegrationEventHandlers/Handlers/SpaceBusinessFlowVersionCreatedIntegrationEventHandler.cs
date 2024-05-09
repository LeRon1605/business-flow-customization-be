using BuildingBlocks.Application.Data;
using BuildingBlocks.EventBus;
using IntegrationEvents.BusinessFlow;
using Submission.Domain.SpaceBusinessFlowAggregate.DomainServices;

namespace Submission.IntegrationEvents.Handlers;

public class SpaceBusinessFlowVersionCreatedIntegrationEventHandler : IntegrationEventHandler<SpaceBusinessFlowVersionCreatedIntegrationEvent>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISpaceBusinessFlowDomainService _spaceBusinessFlowDomainService;
    
    public SpaceBusinessFlowVersionCreatedIntegrationEventHandler(IServiceProvider serviceProvider
        , IUnitOfWork unitOfWork
        , ISpaceBusinessFlowDomainService spaceBusinessFlowDomainService) : base(serviceProvider)
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