using BuildingBlocks.Domain.Models;
using BusinessFlow.Domain.SubmissionExecutionAggregate.Enums;

namespace BusinessFlow.Domain.SubmissionExecutionAggregate.Entities;

public class SubmissionExecutionTask : AuditableEntity
{
    public string Name { get; private set; }
    
    public double Index { get; private set; }
    
    public SubmissionExecutionTaskStatus Status { get; private set; }
    
    public int ExecutionId { get; private set; }
    
    public virtual SubmissionExecution Execution { get; private set; } = null!;
    
    public SubmissionExecutionTask(string name, double index)
    {
        Name = name;
        Index = index;
        Status = SubmissionExecutionTaskStatus.Pending;
    }
    
    public void SetStatus(SubmissionExecutionTaskStatus status)
    {
        Status = status;
    }
    
    private SubmissionExecutionTask()
    {
    }
}