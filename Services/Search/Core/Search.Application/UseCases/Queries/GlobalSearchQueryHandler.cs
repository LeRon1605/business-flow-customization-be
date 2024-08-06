using BuildingBlocks.Application.Cqrs;
using Search.Application.SearchEngines.Services;
using Search.Application.UseCases.Dtos;

namespace Search.Application.UseCases.Queries;

public class GlobalSearchQueryHandler : IQueryHandler<GlobalSearchQuery, GlobalSearchQueryResponse>
{
    private readonly ISpaceSearchService _spaceSearchService;
    private readonly IFormSubmissionSearchService _formSubmissionSearchService;
    
    public GlobalSearchQueryHandler(ISpaceSearchService spaceSearchService, IFormSubmissionSearchService formSubmissionSearchService)
    {
        _spaceSearchService = spaceSearchService;
        _formSubmissionSearchService = formSubmissionSearchService;
    }
    
    public async Task<GlobalSearchQueryResponse> Handle(GlobalSearchQuery request, CancellationToken cancellationToken)
    {
        var spaces = await _spaceSearchService.SearchAsync(request.SearchTerm, request.Page, request.Size);
        var formSubmissions = await _formSubmissionSearchService.SearchAsync(request.SearchTerm, request.Page, request.Size);
        
        return new GlobalSearchQueryResponse()
        {
            Spaces = spaces,
            FormSubmissions = formSubmissions
        };
    }
}