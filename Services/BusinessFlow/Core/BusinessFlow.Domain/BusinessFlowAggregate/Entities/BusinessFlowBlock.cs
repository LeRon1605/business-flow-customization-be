using BuildingBlocks.Domain.Models;
using BusinessFlow.Domain.BusinessFlowAggregate.Enums;
using BusinessFlow.Domain.SubmissionExecutionAggregate.Entities;

namespace BusinessFlow.Domain.BusinessFlowAggregate.Entities;

public class BusinessFlowBlock : Entity
{
    public string Name { get; private set; }
    
    public int BusinessFlowVersionId { get; private set; }
    
    public int? FormId { get; private set; }
    
    public BusinessFlowBlockType Type { get; private set; }
    
    public virtual BusinessFlowVersion BusinessFlowVersion { get; private set; } = null!;
    
    public virtual List<BusinessFlowBranch> FromBranches { get; private set; } = new();
    
    public virtual List<BusinessFlowBranch> ToBranches { get; private set; } = new();

    public virtual List<SubmissionExecution> Executions { get; private set; } = new();
    
    public virtual List<BusinessFlowOutCome> OutComes { get; private set; } = new();
    
    public BusinessFlowBlock(string name, int businessFlowVersionId, BusinessFlowBlockType type)
    {
        Name = name;
        BusinessFlowVersionId = businessFlowVersionId;
        Type = type;
    }
}