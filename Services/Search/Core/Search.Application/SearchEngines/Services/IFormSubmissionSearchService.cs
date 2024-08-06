using BuildingBlocks.Application.Dtos;
using BuildingBlocks.Application.SearchEngines;
using Search.Application.SearchEngines.Models;

namespace Search.Application.SearchEngines.Services;

public interface IFormSubmissionSearchService : ISearchEngineService<FormSubmissionSearchModel>
{
    Task<PagedResultDto<FormSubmissionSearchModel>> SearchAsync(string searchTerm, int page, int size);
}