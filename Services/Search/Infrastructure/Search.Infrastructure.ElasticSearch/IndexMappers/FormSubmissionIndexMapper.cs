using BuildingBlocks.Infrastructure.ElasticSearch.Services;
using Nest;
using Search.Application.SearchEngines.Models;

namespace Search.Infrastructure.ElasticSearch.IndexMappers;

public class FormSubmissionIndexMapper : IIndexMapperProvider<FormSubmissionSearchModel>
{
    public Func<TypeMappingDescriptor<FormSubmissionSearchModel>, ITypeMapping> GetMapping()
    {
        return x => x.AutoMap();
    }
}