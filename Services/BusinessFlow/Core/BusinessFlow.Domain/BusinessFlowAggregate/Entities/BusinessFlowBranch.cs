using BuildingBlocks.Domain.Models;

namespace BusinessFlow.Domain.BusinessFlowAggregate.Entities;

public class BusinessFlowBranch : Entity
{
    public int FromBlockId { get; private set; }
    
    public int ToBlockId { get; private set; }
    
    public int OutComeId { get; private set; }

    public virtual BusinessFlowOutCome OutCome { get; private set; } = null!;
    
    public virtual BusinessFlowBlock FromBlock { get; private set; } = null!;
    
    public virtual BusinessFlowBlock ToBlock { get; private set; } = null!;
    
    public BusinessFlowBranch(int fromBlockId, int toBlockId, int outComeId)
    {
        FromBlockId = fromBlockId;
        ToBlockId = toBlockId;
        OutComeId = outComeId;
    }
}