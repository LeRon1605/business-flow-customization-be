using BuildingBlocks.Domain.Models;

namespace BusinessFlow.Domain.BusinessFlowAggregate.Entities;

public class BusinessFlowBlockPersonInChargeSetting : Entity
{
    public string UserId { get; private set; }
    
    public Guid BusinessFlowBlockId { get; private set; }
    
    public virtual BusinessFlowBlock BusinessFlowBlock { get; private set; } = null!;
    
    public BusinessFlowBlockPersonInChargeSetting(string userId)
    {
        UserId = userId;
    }

    private BusinessFlowBlockPersonInChargeSetting()
    {
        
    }
}