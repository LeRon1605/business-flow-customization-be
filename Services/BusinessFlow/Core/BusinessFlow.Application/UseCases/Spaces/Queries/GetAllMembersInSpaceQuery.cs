using BuildingBlocks.Application.Cqrs;
using BuildingBlocks.Application.Dtos;
using BusinessFlow.Application.UseCases.Spaces.Dtos;

namespace BusinessFlow.Application.UseCases.Spaces.Queries;

public class GetAllMembersInSpaceQuery: PagingAndSortingRequestDto, IQuery<PagedResultDto<SpaceMemberDto>>
{
    public int SpaceId { get; set; }
    public string? Search { get; set; }
    
    public GetAllMembersInSpaceQuery(int spaceId, int page, int size, string? sorting, string? search) : base(page, size, sorting)
    {
        SpaceId = spaceId;
        Search = search;
    }
}