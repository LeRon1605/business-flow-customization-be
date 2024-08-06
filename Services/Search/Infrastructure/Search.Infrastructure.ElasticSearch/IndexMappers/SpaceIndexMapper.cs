using BuildingBlocks.Infrastructure.ElasticSearch.Services;
using Nest;
using Search.Application.SearchEngines.Models;

namespace Search.Infrastructure.ElasticSearch.IndexMappers;

public class SpaceIndexMapper : IIndexMapperProvider<SpaceSearchModel>
{
    public Func<TypeMappingDescriptor<SpaceSearchModel>, ITypeMapping> GetMapping()
    {
        return x => x.Properties(
            p => p.Text(s => s.Name(n => n.Name))
                .Text(s => s.Name(n => n.Description))
                .Keyword(s => s.Name(n => n.Color))
                .Number(s => s.Name(n => n.TenantId).Type(NumberType.Integer))
                .Keyword(s => s.Name(n => n.CreatedBy))
        );
    }
}