using BuildingBlocks.Domain.Models;

namespace Submission.Domain.SubmissionAggregate.Entities;

public class SubmissionDateValue : AuditableEntity
{
    public int SubmissionId { get; private set; }
    
    public int ElementId { get; private set; }
    
    public DateTime Value { get; private set; }

    public virtual FormSubmission Submission { get; private set; } = null!;
    
    public SubmissionDateValue(int submissionId, int elementId, DateTime value)
    {
        SubmissionId = submissionId;
        ElementId = elementId;
        Value = value;
    }

    private SubmissionDateValue()
    {
        
    }
}