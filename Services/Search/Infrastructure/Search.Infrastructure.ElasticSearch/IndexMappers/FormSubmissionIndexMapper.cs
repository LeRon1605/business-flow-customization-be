using BuildingBlocks.Infrastructure.ElasticSearch.Services;
using Nest;
using Search.Application.SearchEngines.Models;

namespace Search.Infrastructure.ElasticSearch.IndexMappers;

public class FormSubmissionIndexMapper : IIndexMapperProvider<FormSubmissionSearchModel>
{
    public Func<TypeMappingDescriptor<FormSubmissionSearchModel>, ITypeMapping> GetMapping()
    {
        return x => x.Properties(
            p => p.Text(s => s.Name(n => n.Name))
                .Number(s => s.Name(n => n.TenantId).Type(NumberType.Integer))
                .Keyword(s => s.Name(n => n.CreatedBy))
        );
    }
}