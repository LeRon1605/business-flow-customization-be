using BuildingBlocks.Application.Dtos;
using Search.Application.SearchEngines.Models;

namespace Search.Application.UseCases.Dtos;

public class GlobalSearchQueryResponse
{
    public PagedResultDto<SpaceSearchModel> Spaces { get; set; } = null!;
    
    public PagedResultDto<FormSubmissionSearchModel> FormSubmissions { get; set; } = null!;
}