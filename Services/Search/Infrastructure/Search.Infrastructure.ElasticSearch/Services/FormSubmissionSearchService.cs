using BuildingBlocks.Application.Dtos;
using BuildingBlocks.Application.Identity;
using BuildingBlocks.Infrastructure.ElasticSearch.Services;
using Microsoft.Extensions.Logging;
using Nest;
using Search.Application.SearchEngines.Models;
using Search.Application.SearchEngines.Services;
using Search.Infrastructure.ElasticSearch.IndexMappers;

namespace Search.Infrastructure.ElasticSearch.Services;

public class FormSubmissionSearchService : SearchEngineService<FormSubmissionSearchModel>, IFormSubmissionSearchService
{
    private readonly ICurrentUser _currentUser;
    private readonly ILogger<FormSubmissionSearchService> _logger;
    
    public FormSubmissionSearchService(IElasticClient client
        , FormSubmissionIndexMapper formSubmissionIndexMapper
        , ICurrentUser currentUser
        , ILogger<FormSubmissionSearchService> logger) : base(client, formSubmissionIndexMapper)
    {
        _currentUser = currentUser;
        _logger = logger;
    }

    public async Task<PagedResultDto<FormSubmissionSearchModel>> SearchAsync(string searchTerm, int page, int size)
    {
        var query = new QueryContainerDescriptor<FormSubmissionSearchModel>()
            .Bool(b => b
                .Must(
                    m => m
                        .Match(m => m
                            .Field(c => c.Name)
                            .Query(searchTerm)),
                    m => m.Term(te => te
                        .Field(f => f.TenantId)
                        .Value(_currentUser.TenantId)),
                    m => m.Term(te => te
                        .Field(f => f.CreatedBy)
                        .Value(_currentUser.Id))
                ));
        
        var searchResponse = await Client.SearchAsync<FormSubmissionSearchModel>(s => s
            .Index(IndexName)
            .Query(_ => query)
            .Highlight(h => h
                .PreTags("<b>")
                .PostTags("</b>")
                .FragmentSize(100)
                .Fragmenter(HighlighterFragmenter.Span)
                .NumberOfFragments(5)
                .Fields(
                    f => f.Field(c => c.Name).Type(HighlighterType.Plain)))
            .From((page - 1) * size)
            .Size(size)
        );

        if (!searchResponse.IsValid)
        {
            _logger.LogError("Error while searching for spaces. Error: {Error}", searchResponse.OriginalException.Message);
            return new PagedResultDto<FormSubmissionSearchModel>(0, size, new List<FormSubmissionSearchModel>());   
        }

        var result = new List<FormSubmissionSearchModel>();
        foreach (var hit in searchResponse.Hits)
        {
            var document = hit.Source;

            foreach (var highlight in hit.Highlight)
            {
                if (highlight.Key == "name")
                {
                    document.Name = string.Join("...", highlight.Value);
                }
            }
            
            result.Add(document);
        }

        return new PagedResultDto<FormSubmissionSearchModel>(searchResponse.Total, size, result);
    }
}