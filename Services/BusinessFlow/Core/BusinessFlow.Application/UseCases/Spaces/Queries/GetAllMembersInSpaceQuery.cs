using BuildingBlocks.Application.Cqrs;
using BuildingBlocks.Application.Dtos;
using BusinessFlow.Application.UseCases.Spaces.Dtos;

namespace BusinessFlow.Application.UseCases.Spaces.Queries;

public class GetAllMembersInSpaceQuery: IQuery<List<SpaceMemberDto>>
{
    public int SpaceId { get; set; }
    
    public GetAllMembersInSpaceQuery(int spaceId)
    {
        SpaceId = spaceId;
    }
}