using BuildingBlocks.Application.Data;
using Search.Application.SearchEngines.Services;

namespace Search.Infrastructure.ElasticSearch;

public class IndexDataSeeder : IDataSeeder
{
    public int Id => 1;
    private readonly ISpaceSearchService _spaceSearchService;
    private readonly IFormSubmissionSearchService _formSubmissionSearchService;
    
    public IndexDataSeeder(ISpaceSearchService spaceSearchService, IFormSubmissionSearchService formSubmissionSearchService)
    {
        _spaceSearchService = spaceSearchService;
        _formSubmissionSearchService = formSubmissionSearchService;
    }
    
    public async Task SeedAsync()
    {
        await _spaceSearchService.CreateIndexAsync();
        await _formSubmissionSearchService.CreateIndexAsync();
    }
}