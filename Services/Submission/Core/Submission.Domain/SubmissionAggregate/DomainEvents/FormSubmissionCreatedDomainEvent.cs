using BuildingBlocks.Domain.Events;
using Submission.Domain.SubmissionAggregate.Entities;

namespace Submission.Domain.SubmissionAggregate.DomainEvents;

public class FormSubmissionCreatedDomainEvent : IDomainEvent
{
    public FormSubmission FormSubmission { get; set; }
    
    public FormSubmissionCreatedDomainEvent(FormSubmission formSubmission)
    {
        FormSubmission = formSubmission;
    }
}