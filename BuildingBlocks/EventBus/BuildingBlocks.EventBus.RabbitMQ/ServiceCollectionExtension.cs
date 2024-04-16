using BuildingBlocks.EventBus.Abstracts;
using BuildingBlocks.Shared.Extensions;
using BuildingBlocks.Shared.Helpers;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlocks.EventBus.RabbitMQ;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddRabbitMq(this IServiceCollection services, IConfiguration configuration)
    {
        // {
        //   "EventBus": {
        //     "HostName": "...",
        //     "Queue": "...",
        //     "UserName": "...",
        //     "Password": "...",
        //     "Port": "...",
        //     "Retry": "..."
        //   }
        // }

        var eventBusSection = configuration.GetSection("EventBus");
        
        var queue = AssemblyHelper.GetServiceName();
        var hostName = eventBusSection.GetRequiredValue("HostName");
        var userName = eventBusSection.GetRequiredValue("UserName");
        var password = eventBusSection.GetRequiredValue("Password");
        var port = eventBusSection.GetRequiredValue<int>("Port");
        var retry = eventBusSection.GetRequiredValue<int>("Retry");
        
        services.AddMassTransit(x =>
        {
            var consumersAssemblies = AssemblyHelper.GetReferencedAssembliesByType(typeof(IIntegrationEventHandler));
            if (consumersAssemblies.Any())
                x.AddConsumers(consumersAssemblies);
            
            x.UsingRabbitMq((context, config) =>
            {
                config.Host(hostName, host =>
                {
                    host.Username(userName);
                    host.Password(password);
                });
                
                config.UseMessageRetry(r => r.Immediate(retry));
                
                config.ReceiveEndpoint(queue, e => e.ConfigureConsumers(context));
                config.ConfigureEndpoints(context);
            });
        });

        services.AddScoped<IEventPublisher, EventPublisher>();

        return services;
    }
}