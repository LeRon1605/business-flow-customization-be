using BuildingBlocks.Application.SearchEngines;
using BuildingBlocks.Infrastructure.ElasticSearch.Services;
using BuildingBlocks.Infrastructure.ElasticSearch.Settings;
using BuildingBlocks.Shared.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest;

namespace BuildingBlocks.Infrastructure.ElasticSearch;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddElasticSearch(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptionSetting<ElasticSearchSetting>(configuration, nameof(ElasticSearchSetting));
        
        var elasticSearchSetting = configuration.GetSetting<ElasticSearchSetting>(nameof(ElasticSearchSetting));

        var setting = new ConnectionSettings(new Uri(elasticSearchSetting.Url));

        setting.BasicAuthentication(elasticSearchSetting.UserName, elasticSearchSetting.Password);

        var client = new ElasticClient(setting);

        services.AddSingleton<IElasticClient>(client);
        services.AddScoped(typeof(ISearchEngineService<>), typeof(SearchEngineService<>));
        
        return services;    
    }
}