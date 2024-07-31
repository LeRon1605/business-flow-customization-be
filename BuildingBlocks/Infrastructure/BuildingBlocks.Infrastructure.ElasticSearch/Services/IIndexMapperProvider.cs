using BuildingBlocks.Application.SearchEngines.Models;
using Nest;

namespace BuildingBlocks.Infrastructure.ElasticSearch.Services;

public interface IIndexMapperProvider<T> where T : BaseSearchModel
{
    Func<TypeMappingDescriptor<T>, ITypeMapping> GetMapping();
}