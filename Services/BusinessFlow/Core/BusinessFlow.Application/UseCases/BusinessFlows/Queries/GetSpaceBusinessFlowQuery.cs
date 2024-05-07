using BuildingBlocks.Application.Cqrs;
using BusinessFlow.Application.UseCases.BusinessFlows.Dtos;

namespace BusinessFlow.Application.UseCases.BusinessFlows.Queries;

public class GetSpaceBusinessFlowQuery : IQuery<BusinessFlowDto>
{
    public int Id { get; set; }
    public int SpaceId { get; set; }
    
    public GetSpaceBusinessFlowQuery(int id, int spaceId)
    {
        Id = id;
        SpaceId = spaceId;
    }
}