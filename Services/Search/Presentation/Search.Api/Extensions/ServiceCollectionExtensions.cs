using BuildingBlocks.Application.Data;
using BuildingBlocks.Infrastructure.ElasticSearch;
using Search.Infrastructure.ElasticSearch;
using Search.Infrastructure.ElasticSearch.IndexMappers;

namespace Search.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static WebApplicationBuilder AddElasticSearch(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddElasticSearch(builder.Configuration);
        
        builder.Services
            .AddScoped<IDataSeeder, IndexDataSeeder>()
            .AddScoped<SpaceIndexMapper>()
            .AddScoped<FormSubmissionIndexMapper>();
        
        return builder;
    }
}