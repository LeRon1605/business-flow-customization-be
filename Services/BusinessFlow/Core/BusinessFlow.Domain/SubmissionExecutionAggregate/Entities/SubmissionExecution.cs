using BuildingBlocks.Domain.Models;
using BusinessFlow.Domain.BusinessFlowAggregate.Entities;
using BusinessFlow.Domain.SubmissionExecutionAggregate.DomainEvents;
using BusinessFlow.Domain.SubmissionExecutionAggregate.Enums;
using BusinessFlow.Domain.SubmissionExecutionAggregate.Exceptions;

namespace BusinessFlow.Domain.SubmissionExecutionAggregate.Entities;

public class SubmissionExecution : AuditableTenantAggregateRoot
{
    public Guid BusinessFlowBlockId { get; private set; }
    
    public int SubmissionId { get; private set; }
    
    public Guid? OutComeId { get; private set; }
    
    public SubmissionExecutionStatus Status { get; private set; }
    
    public string? CompletedBy { get; private set; }
    
    public DateTime? CompletedAt { get; private set; }
    
    public virtual BusinessFlowBlock BusinessFlowBlock { get; private set; } = null!;
    
    public virtual BusinessFlowOutCome? OutCome { get; private set; }
    
    public virtual List<SubmissionExecutionPersonInCharge> PersonInCharges { get; private set; } = new();
    
    public virtual List<SubmissionExecutionTask> Tasks { get; private set; } = new();
    
    public SubmissionExecution(Guid businessFlowBlockId, int submissionId)
    {
        BusinessFlowBlockId = businessFlowBlockId;
        SubmissionId = submissionId;
        Status = SubmissionExecutionStatus.InProgress;
    }

    public void AddTask(string name, double index)
    {
        Tasks.Add(new SubmissionExecutionTask(name, index));
    }
    
    public void AddPersonInCharge(string personInChargeId)
    {
        PersonInCharges.Add(new SubmissionExecutionPersonInCharge(personInChargeId));
    }
    
    public void MarkCompleted(Guid? outComeId, string completedBy)
    {
        OutComeId = outComeId;
        CompletedBy = completedBy;
        CompletedAt = DateTime.UtcNow;
        Status = SubmissionExecutionStatus.Completed;
    }
    
    public void SetTaskStatus(int taskId, SubmissionExecutionTaskStatus status)
    {
        var task = Tasks.FirstOrDefault(x => x.Id == taskId);
        if (task == null)
        {
            throw new ExecutionTaskNotFoundException(Id, taskId);
        }
        
        task.SetStatus(status);
    }

    private SubmissionExecution()
    {
        
    }
}