using BuildingBlocks.Domain.Models;
using BusinessFlow.Domain.BusinessFlowAggregate.Enums;
using BusinessFlow.Domain.SpaceAggregate.Entities;

namespace BusinessFlow.Domain.BusinessFlowAggregate.Entities;

public class BusinessFlowVersion : AuditableTenantAggregateRoot
{
    public int SpaceId { get; private set; }
    
    public BusinessFlowStatus Status { get; private set; }
    
    public virtual Space Space { get; private set; } = null!;
    
    public virtual List<BusinessFlowBlock> Blocks { get; private set; } = new();
    
    public BusinessFlowVersion(int spaceId)
    {
        Status = BusinessFlowStatus.Draft;
        SpaceId = spaceId;
    }
}