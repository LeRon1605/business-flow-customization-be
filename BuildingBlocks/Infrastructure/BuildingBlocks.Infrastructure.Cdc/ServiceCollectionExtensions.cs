using BuildingBlocks.Infrastructure.Cdc.Settings;
using BuildingBlocks.Infrastructure.Cdc.States;
using BuildingBlocks.Shared.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlocks.Infrastructure.Cdc;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCdcService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptionSetting<CdcTrackingSetting>(configuration, nameof(CdcTrackingSetting));
        services.AddSingleton<ICdcStateService, CdcStateService>();
        services.AddHostedService<CdcTrackingService>();
        
        return services;
    }
}