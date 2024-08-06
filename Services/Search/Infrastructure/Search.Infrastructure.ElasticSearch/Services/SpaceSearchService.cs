using BuildingBlocks.Application.Dtos;
using BuildingBlocks.Application.Identity;
using BuildingBlocks.Infrastructure.ElasticSearch.Services;
using Microsoft.Extensions.Logging;
using Nest;
using Search.Application.SearchEngines.Models;
using Search.Application.SearchEngines.Services;
using Search.Infrastructure.ElasticSearch.IndexMappers;

namespace Search.Infrastructure.ElasticSearch.Services;

public class SpaceSearchService : SearchEngineService<SpaceSearchModel>, ISpaceSearchService
{
    private readonly ICurrentUser _currentUser;
    private readonly ILogger<SpaceSearchService> _logger;
    
    public SpaceSearchService(IElasticClient client
        , SpaceIndexMapper spaceIndexMapper
        , ICurrentUser currentUser
        , ILogger<SpaceSearchService> logger) : base(client, spaceIndexMapper)
    {
        _currentUser = currentUser;
        _logger = logger;
    }
    
    public async Task<PagedResultDto<SpaceSearchModel>> SearchAsync(string searchTerm, int page, int size)
    {
        var query = new QueryContainerDescriptor<SpaceSearchModel>()
            .Bool(b => b
                .Must(
                    m => m
                        .MultiMatch(ma => ma
                            .Fields(f => f
                                .Field(c => c.Name, 3)
                                .Field(c => c.Description, 2))
                            .Query(searchTerm)),
                    m => m.Term(te => te
                        .Field(f => f.TenantId)
                        .Value(_currentUser.TenantId))
                ));
        
        var searchResponse = await Client.SearchAsync<SpaceSearchModel>(s => s
            .Index(IndexName)
            .Query(_ => query)
            .Highlight(h => h
                .PreTags("<b>")
                .PostTags("</b>")
                .FragmentSize(100)
                .Fragmenter(HighlighterFragmenter.Span)
                .NumberOfFragments(5)
                .Fields(
                    f => f.Field(c => c.Name).Type(HighlighterType.Plain),
                    f => f.Field(c => c.Description).Type(HighlighterType.Plain)))
            .From((page - 1) * size)
            .Size(size)
        );

        if (!searchResponse.IsValid)
        {
            _logger.LogError("Error while searching for spaces. Error: {Error}", searchResponse.OriginalException.Message);
            return new PagedResultDto<SpaceSearchModel>(0, size, new List<SpaceSearchModel>());   
        }
        
        var result = new List<SpaceSearchModel>();
        foreach (var hit in searchResponse.Hits)
        {
            var document = hit.Source;

            foreach (var highlight in hit.Highlight)
            {
                if (highlight.Key == "name")
                {
                    document.Name = string.Join("...", highlight.Value);
                }
                else if (highlight.Key == "description")
                {
                    document.Description = string.Join("...", highlight.Value);
                }
            }
            
            result.Add(document);
        }

        return new PagedResultDto<SpaceSearchModel>(searchResponse.Total, size, result);
    }
}