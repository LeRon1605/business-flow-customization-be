using BuildingBlocks.Domain.Events;
using BusinessFlow.Domain.SubmissionExecutionAggregate.Entities;

namespace BusinessFlow.Domain.SubmissionExecutionAggregate.DomainEvents;

public class SubmissionExecutionCreatedDomainEvent : IDomainEvent
{
    public SubmissionExecution SubmissionExecution { get; set; }
    
    public SubmissionExecutionCreatedDomainEvent(SubmissionExecution submissionExecution)
    {
        SubmissionExecution = submissionExecution;
    }
}