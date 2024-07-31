using BuildingBlocks.Application.Identity;
using BuildingBlocks.EventBus;
using BuildingBlocks.EventBus.Enums;
using IntegrationEvents.Cdc;
using Microsoft.Extensions.Logging;
using Search.Application.SearchEngines.Models;
using Search.Application.SearchEngines.Services;

namespace Search.IntegrationEventHandlers.Handlers;

public class SpaceEntityTrackingIntegrationEventHandler : IntegrationEventHandler<SpaceEntityTrackingIntegrationEvent>
{
    private readonly ISpaceSearchService _spaceSearchService;
    public SpaceEntityTrackingIntegrationEventHandler(ICurrentUser currentUser
        , ILogger<IntegrationEventHandler<SpaceEntityTrackingIntegrationEvent>> logger
        , ISpaceSearchService spaceSearchService) : base(currentUser, logger)
    {
        _spaceSearchService = spaceSearchService;
    }

    public override async Task HandleAsync(SpaceEntityTrackingIntegrationEvent @event)
    {
        var model = new SpaceSearchModel()
        {
            Id = @event.Id.ToString(),
            Name = @event.Name,
            Description = @event.Description,
            Color = @event.Color,
            TenantId = @event.TenantId,
            CreatedBy = @event.UserId
        };
        
        switch (@event.Action)
        {
            case EntityAction.Insert:
                await _spaceSearchService.InsertAsync(model);

                break;
            
            case EntityAction.Update:
                await _spaceSearchService.UpdateAsync(model);
                break;
            
            case EntityAction.Delete:
                await _spaceSearchService.DeleteAsync(@event.Id.ToString());
                break;
        }
    }
}