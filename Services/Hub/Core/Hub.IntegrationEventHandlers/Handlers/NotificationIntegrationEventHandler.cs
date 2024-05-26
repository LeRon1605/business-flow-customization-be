using BuildingBlocks.Application.Identity;
using BuildingBlocks.EventBus;
using Hub.Application.Clients;
using Hub.Application.Clients.Abstracts;
using Hub.Application.Services.Abstracts;
using IntegrationEvents.Hub;
using Microsoft.Extensions.Logging;

namespace Hub.IntegrationEventHandlers.Handlers;

public class NotificationIntegrationEventHandler : IntegrationEventHandler<NotificationIntegrationEvent>
{
    private readonly INotificationSenderService _notificationSenderService;
    
    public NotificationIntegrationEventHandler(ICurrentUser currentUser
        , ILogger<IntegrationEventHandler<NotificationIntegrationEvent>> logger
        , INotificationSenderService notificationSenderService) : base(currentUser, logger)
    {
        _notificationSenderService = notificationSenderService;
    }

    public override async Task HandleAsync(NotificationIntegrationEvent @event)
    {
        if (@event.TenantId == null)
            return;
        
        await _notificationSenderService.SendAsync(@event.ReceiverIds
            , @event.TenantId.Value
            , @event.Data
            , @event.Type);
    }
}