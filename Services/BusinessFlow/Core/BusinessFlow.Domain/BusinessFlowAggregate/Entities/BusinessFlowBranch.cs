using BuildingBlocks.Domain.Models;
using BusinessFlow.Domain.BusinessFlowAggregate.Models;

namespace BusinessFlow.Domain.BusinessFlowAggregate.Entities;

public class BusinessFlowBranch : Entity
{
    public Guid FromBlockId { get; private set; }
    
    public Guid ToBlockId { get; private set; }
    
    public Guid? OutComeId { get; private set; }

    public virtual BusinessFlowOutCome? OutCome { get; private set; }
    
    public virtual BusinessFlowBlock FromBlock { get; private set; } = null!;
    
    public virtual BusinessFlowBlock ToBlock { get; private set; } = null!;

    public BusinessFlowBranch(BusinessFlowBranchModel model)
    {
        FromBlockId = model.FromBlockId;
        ToBlockId = model.ToBlockId;
        OutComeId = model.OutComeId;
    }

    private BusinessFlowBranch()
    {
        
    }
}