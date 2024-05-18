using BuildingBlocks.Domain.Models;

namespace BusinessFlow.Domain.SubmissionExecutionAggregate.Entities;

public class SubmissionExecutionPersonInCharge : Entity
{
    public string UserId { get; private set; }
    
    public int ExecutionId { get; private set; }
    
    public virtual SubmissionExecution Execution { get; private set; } = null!;
    
    public SubmissionExecutionPersonInCharge(string userId)
    {
        UserId = userId;
    }
    
    private SubmissionExecutionPersonInCharge()
    {
        
    }
}