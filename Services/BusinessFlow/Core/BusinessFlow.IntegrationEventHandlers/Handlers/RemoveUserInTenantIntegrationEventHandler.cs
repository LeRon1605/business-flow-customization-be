using BuildingBlocks.Application.Data;
using BuildingBlocks.Application.Identity;
using IntegrationEvents.Identity;
using BuildingBlocks.EventBus;
using BusinessFlow.Domain.SpaceAggregate.DomainServices;
using Microsoft.Extensions.Logging;

namespace BusinessFlow.IntegrationEventHandler.Handlers;

public class RemoveUserInTenantIntegrationEventHandler : IntegrationEventHandler<RemoveUserInTenantIntegrationEvent>
{
    private readonly ISpaceDomainService _spaceDomainService;
    private readonly IUnitOfWork _unitOfWork;
    
    public RemoveUserInTenantIntegrationEventHandler(ICurrentUser currentUser
        , ILogger<IntegrationEventHandler<RemoveUserInTenantIntegrationEvent>> logger
        , ISpaceDomainService spaceDomainService
        , IUnitOfWork unitOfWork) : base(currentUser, logger)
    {
        _spaceDomainService = spaceDomainService;
        _unitOfWork = unitOfWork;
    }

    public override async Task HandleAsync(RemoveUserInTenantIntegrationEvent @event)
    {
        await _spaceDomainService.RemoveUserInSpaceAsync(@event.DeletedUserId);
        await _unitOfWork.CommitAsync();
    }
}   