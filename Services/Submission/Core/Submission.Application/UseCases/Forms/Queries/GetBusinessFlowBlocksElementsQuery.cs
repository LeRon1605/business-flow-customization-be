using Application.Dtos.Submissions.Responses;
using BuildingBlocks.Application.Cqrs;

namespace Submission.Application.UseCases.Forms.Queries;

public class GetBusinessFlowBlocksElementsQuery : IQuery<BusinessFlowBlocksElementsResponse>
{
    public int SpaceId { get; set; }
    
    public List<Guid> BusinessFlowBlockIds { get; set; }
    
    public GetBusinessFlowBlocksElementsQuery(int spaceId, List<Guid> businessFlowBlockIds)
    {
        SpaceId = spaceId;
        BusinessFlowBlockIds = businessFlowBlockIds;
    }
}