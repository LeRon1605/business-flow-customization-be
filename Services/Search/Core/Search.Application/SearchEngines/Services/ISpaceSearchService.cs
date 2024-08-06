using BuildingBlocks.Application.Dtos;
using BuildingBlocks.Application.SearchEngines;
using Search.Application.SearchEngines.Models;

namespace Search.Application.SearchEngines.Services;

public interface ISpaceSearchService : ISearchEngineService<SpaceSearchModel>
{
    Task<PagedResultDto<SpaceSearchModel>> SearchAsync(string searchTerm, int page, int size);
}