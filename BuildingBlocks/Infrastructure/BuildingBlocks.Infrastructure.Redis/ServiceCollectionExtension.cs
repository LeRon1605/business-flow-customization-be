using BuildingBlocks.Application.Caching.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlocks.Infrastructure.Redis;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddRedisCache(this IServiceCollection services, IConfiguration configuration, string instanceName)
    {
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration.GetConnectionString("Redis");
            options.InstanceName = instanceName;
        });

        services.AddScoped<ICachingService, CachingService>();

        return services;
    }
}