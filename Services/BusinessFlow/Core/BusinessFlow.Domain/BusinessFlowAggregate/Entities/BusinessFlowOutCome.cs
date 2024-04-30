using BuildingBlocks.Domain.Models;
using BusinessFlow.Domain.SubmissionExecutionAggregate.Entities;

namespace BusinessFlow.Domain.BusinessFlowAggregate.Entities;

public class BusinessFlowOutCome : Entity
{
    public string Name { get; private set; }
    
    public string Color { get; private set; }
    
    public int BusinessFlowBlockId { get; private set; }
    
    public virtual BusinessFlowBlock BusinessFlowBlock { get; private set; } = null!;
    
    public virtual List<BusinessFlowBranch> Branches { get; private set; } = new();
    
    public virtual List<SubmissionExecution> Executions { get; private set; } = new();
    
    public BusinessFlowOutCome(string name, string color, int businessFlowBlockId)
    {
        Name = name;
        Color = color;
        BusinessFlowBlockId = businessFlowBlockId;
    }
}