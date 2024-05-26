using BuildingBlocks.Application.Identity;
using BuildingBlocks.EventBus;
using Hub.Application.Services.Abstracts;
using IntegrationEvents.Hub;
using Microsoft.Extensions.Logging;

namespace Hub.IntegrationEventHandlers.Handlers;

public class RealTimeIntegrationEventHandler : IntegrationEventHandler<RealTimeIntegrationEvent>
{
    private readonly INotificationSenderService _notificationSenderService;
    
    public RealTimeIntegrationEventHandler(ICurrentUser currentUser
        , ILogger<IntegrationEventHandler<RealTimeIntegrationEvent>> logger
        , INotificationSenderService notificationSenderService) : base(currentUser, logger)
    {
        _notificationSenderService = notificationSenderService;
    }

    public override async Task HandleAsync(RealTimeIntegrationEvent @event)
    {
        if (@event.TenantId == null)
            return;
        
        await _notificationSenderService.SendRealTimeAsync(@event.ReceiverIds
            , @event.TenantId.Value
            , @event.Data
            , @event.Type);
    }
}