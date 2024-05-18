using BuildingBlocks.Domain.Models;

namespace BusinessFlow.Domain.SubmissionExecutionAggregate.Entities;

public class SubmissionExecutionTask : AuditableEntity
{
    public string Name { get; private set; }
    
    public double Index { get; private set; }
    
    public int ExecutionId { get; private set; }
    
    public virtual SubmissionExecution Execution { get; private set; } = null!;
    
    public SubmissionExecutionTask(string name, double index)
    {
        Name = name;
        Index = index;
    }
    
    private SubmissionExecutionTask()
    {
    }
}