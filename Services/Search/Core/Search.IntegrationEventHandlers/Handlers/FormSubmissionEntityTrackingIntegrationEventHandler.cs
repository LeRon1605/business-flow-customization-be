using BuildingBlocks.Application.Identity;
using BuildingBlocks.EventBus;
using BuildingBlocks.EventBus.Enums;
using IntegrationEvents.Cdc;
using Microsoft.Extensions.Logging;
using Search.Application.SearchEngines.Models;
using Search.Application.SearchEngines.Services;

namespace Search.IntegrationEventHandlers.Handlers;

public class FormSubmissionEntityTrackingIntegrationEventHandler : IntegrationEventHandler<FormSubmissionEntityTrackingIntegrationEvent>
{
    private readonly IFormSubmissionSearchService _formSubmissionSearchService;
    
    public FormSubmissionEntityTrackingIntegrationEventHandler(ICurrentUser currentUser
        , ILogger<IntegrationEventHandler<FormSubmissionEntityTrackingIntegrationEvent>> logger
        , IFormSubmissionSearchService formSubmissionSearchService) : base(currentUser, logger)
    {
        _formSubmissionSearchService = formSubmissionSearchService;
    }

    public override async Task HandleAsync(FormSubmissionEntityTrackingIntegrationEvent @event)
    {
        var model = new FormSubmissionSearchModel()
        {
            Id = @event.Id.ToString(),
            Name = @event.Name,
            TenantId = @event.TenantId,
            CreatedBy = @event.UserId
        };
        
        switch (@event.Action)
        {
            case EntityAction.Insert:
                await _formSubmissionSearchService.InsertAsync(model);
                break;
            
            case EntityAction.Update:
                await _formSubmissionSearchService.UpdateAsync(model);
                break;
            
            case EntityAction.Delete:
                await _formSubmissionSearchService.DeleteAsync(@event.Id.ToString());
                break;
        }
    }
}