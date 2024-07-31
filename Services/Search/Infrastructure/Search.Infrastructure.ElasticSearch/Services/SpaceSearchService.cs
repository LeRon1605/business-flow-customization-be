using BuildingBlocks.Infrastructure.ElasticSearch.Services;
using Nest;
using Search.Application.SearchEngines.Models;
using Search.Application.SearchEngines.Services;
using Search.Infrastructure.ElasticSearch.IndexMappers;

namespace Search.Infrastructure.ElasticSearch.Services;

public class SpaceSearchService : SearchEngineService<SpaceSearchModel>, ISpaceSearchService
{
    public SpaceSearchService(IElasticClient client, SpaceIndexMapper spaceIndexMapper) : base(client, spaceIndexMapper)
    {
    }
}