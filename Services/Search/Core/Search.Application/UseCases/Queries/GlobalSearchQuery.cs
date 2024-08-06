using BuildingBlocks.Application.Cqrs;
using BuildingBlocks.Application.Dtos;
using Search.Application.UseCases.Dtos;

namespace Search.Application.UseCases.Queries;

public class GlobalSearchQuery : PagingRequestDto, IQuery<GlobalSearchQueryResponse>
{
    public string SearchTerm { get; set; }
    
    public GlobalSearchQuery(string searchTerm, int page, int size) 
    {
        SearchTerm = searchTerm;
    }
}