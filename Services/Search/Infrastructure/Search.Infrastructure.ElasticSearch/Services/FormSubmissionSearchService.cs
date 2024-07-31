using BuildingBlocks.Infrastructure.ElasticSearch.Services;
using Nest;
using Search.Application.SearchEngines.Models;
using Search.Application.SearchEngines.Services;
using Search.Infrastructure.ElasticSearch.IndexMappers;

namespace Search.Infrastructure.ElasticSearch.Services;

public class FormSubmissionSearchService : SearchEngineService<FormSubmissionSearchModel>, IFormSubmissionSearchService
{
    public FormSubmissionSearchService(IElasticClient client, FormSubmissionIndexMapper formSubmissionIndexMapper) 
        : base(client, formSubmissionIndexMapper)
    {
    }
}