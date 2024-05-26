using BuildingBlocks.Application.Identity;
using BuildingBlocks.EventBus.Abstracts;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.EventBus;

public abstract class IntegrationEventHandler<TEvent> : IIntegrationEventHandler<TEvent> where TEvent : class, IIntegrationEvent
{
    protected ICurrentUser CurrentUser;
    protected ILogger<IntegrationEventHandler<TEvent>> Logger;
    
    public IntegrationEventHandler(ICurrentUser currentUser, ILogger<IntegrationEventHandler<TEvent>> logger)
    {
        CurrentUser = currentUser;
        Logger = logger;
    }
    
    public async Task Consume(ConsumeContext<TEvent> context)
    {
        if (context.Message.UserId != null && context.Message.TenantId != null)
        {
            CurrentUser.IsAuthenticated = true;
            CurrentUser.Id = context.Message.UserId;
            CurrentUser.TenantId = context.Message.TenantId.Value;
        }
        
        Logger.LogInformation("Handling integration event: {EventName}", context.Message.GetType().Name);
        
        await HandleAsync(context.Message);
        
        Logger.LogInformation("Handled integration event: {EventName}", context.Message.GetType().Name);
    }
    
    public abstract Task HandleAsync(TEvent @event);
}