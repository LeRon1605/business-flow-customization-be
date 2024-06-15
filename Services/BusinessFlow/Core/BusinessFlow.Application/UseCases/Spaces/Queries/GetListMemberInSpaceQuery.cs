using BuildingBlocks.Application.Cqrs;
using BusinessFlow.Application.UseCases.Spaces.Dtos;

namespace BusinessFlow.Application.UseCases.Spaces.Queries;

public class GetListMemberInSpaceQuery : IQuery<List<SpaceMemberDto>>
{
    public int SpaceId { get; set; }
    public GetListMemberInSpaceQuery(int spaceId)
    {
        SpaceId = spaceId;
    }
}