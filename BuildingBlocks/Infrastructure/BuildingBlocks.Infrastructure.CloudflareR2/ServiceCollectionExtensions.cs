using BuildingBlocks.Application.FileUploader;
using BuildingBlocks.Shared.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlocks.Infrastructure.CloudflareR2;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCloudflareR2(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptionSetting<CloudflareSetting>(configuration, nameof(CloudflareSetting));
        services.AddScoped<IFileUploader, CloudflareFileUploader>();
        return services;
    }
}