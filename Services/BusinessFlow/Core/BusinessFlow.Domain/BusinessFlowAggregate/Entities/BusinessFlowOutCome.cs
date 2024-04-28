using BuildingBlocks.Domain.Models;
using BusinessFlow.Domain.SubmissionExecutionAggregate.Entities;

namespace BusinessFlow.Domain.BusinessFlowAggregate.Entities;

public class BusinessFlowOutCome : Entity
{
    public string Name { get; private set; }
    
    public string Color { get; private set; }
    
    public virtual BusinessFlowBranch Branch { get; private set; } = null!;
    
    public virtual List<SubmissionExecution> Executions { get; private set; } = new();
    
    public BusinessFlowOutCome(string name, string color)
    {
        Name = name;
        Color = color;
    }
}