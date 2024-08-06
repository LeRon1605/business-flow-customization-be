using BuildingBlocks.Application.Dtos;

namespace Search.Application.UseCases.Dtos;

public class GlobalSearchQueryRequest : PagingRequestDto
{
    public string SearchTerm { get; set; } = null!;
}