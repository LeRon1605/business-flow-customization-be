using BuildingBlocks.Application.Dtos;
using BuildingBlocks.Domain.Specifications;

namespace BusinessFlow.Domain.SpaceAggregate.Specifications;

public class FilterAndPagingSpaceMemberRequestDto : PagingAndSortingRequestDto
{
    public string? Search { get; set; }
}