using BuildingBlocks.Application.Cqrs;
using BusinessFlow.Application.UseCases.BusinessFlows.Dtos;

namespace BusinessFlow.Application.UseCases.BusinessFlows.Queries;

public class GetLatestSpaceBusinessFlowOutComeQuery : IQuery<List<BusinessFlowBlockOutComeDto>>
{
    public int SpaceId { get; set; }
    
    public GetLatestSpaceBusinessFlowOutComeQuery(int spaceId)
    {
        SpaceId = spaceId;
    }
}