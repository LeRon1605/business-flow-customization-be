using BuildingBlocks.Domain.Models;

namespace Submission.Domain.SpaceBusinessFlowAggregate.Entities;

public class SpaceBusinessFlow : AuditableTenantAggregateRoot
{
    public int SpaceId { get; private set; }
    
    public int BusinessFlowVersionId { get; private set; }
    
    public SpaceBusinessFlow(int spaceId, int businessFlowVersionId)
    {
        SpaceId = spaceId;
        BusinessFlowVersionId = businessFlowVersionId;
    }
    
    public void Update(int businessFlowVersionId)
    {
        BusinessFlowVersionId = businessFlowVersionId;
    }
    
    private SpaceBusinessFlow()
    {
        
    }
}