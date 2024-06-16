using BuildingBlocks.Domain.Events;
using Submission.Domain.SubmissionAggregate.Entities;

namespace Submission.Domain.SubmissionAggregate.DomainEvents;

public class FormSubmissionTrackingEmailCreatedDomainEvent : IDomainEvent
{
    public FormSubmission FormSubmission { get; set; }
    
    public FormSubmissionTrackingEmailCreatedDomainEvent(FormSubmission formSubmission)
    {
        FormSubmission = formSubmission;
    }
}