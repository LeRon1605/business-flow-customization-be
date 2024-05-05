using BuildingBlocks.Domain.Models;
using BusinessFlow.Domain.BusinessFlowAggregate.Models;
using BusinessFlow.Domain.SubmissionExecutionAggregate.Entities;

namespace BusinessFlow.Domain.BusinessFlowAggregate.Entities;

public class BusinessFlowOutCome : Entity<Guid>
{
    public string Name { get; private set; }
    
    public string Color { get; private set; }
    
    public Guid BusinessFlowBlockId { get; private set; }
    
    public virtual BusinessFlowBlock BusinessFlowBlock { get; private set; } = null!;
    
    public virtual List<BusinessFlowBranch> Branches { get; private set; } = new();
    
    public virtual List<SubmissionExecution> Executions { get; private set; } = new();
    
    public BusinessFlowOutCome(BusinessFlowOutComeModel model)
    {
        Id = model.Id;
        Name = model.Name;
        Color = model.Color;
    }
    
    private BusinessFlowOutCome()
    {
        
    }
}