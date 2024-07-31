using BuildingBlocks.Infrastructure.ElasticSearch.Services;
using Nest;
using Search.Application.SearchEngines.Models;

namespace Search.Infrastructure.ElasticSearch.IndexMappers;

public class SpaceIndexMapper : IIndexMapperProvider<SpaceSearchModel>
{
    public Func<TypeMappingDescriptor<SpaceSearchModel>, ITypeMapping> GetMapping()
    {
        return x => x.AutoMap();
    }
}