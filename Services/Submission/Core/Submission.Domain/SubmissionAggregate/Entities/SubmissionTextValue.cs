using BuildingBlocks.Domain.Models;

namespace Submission.Domain.SubmissionAggregate.Entities;

public class SubmissionTextValue : AuditableEntity
{
    public int SubmissionId { get; private set; }
    
    public int ElementId { get; private set; }
    
    public string Value { get; private set; }
    
    public virtual FormSubmission Submission { get; private set; } = null!;
    
    public SubmissionTextValue(int submissionId, int elementId, string value)
    {
        SubmissionId = submissionId;
        ElementId = elementId;
        Value = value;
    }
}