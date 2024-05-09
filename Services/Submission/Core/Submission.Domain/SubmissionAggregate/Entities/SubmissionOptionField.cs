using BuildingBlocks.Domain.Models;

namespace Submission.Domain.SubmissionAggregate.Entities;

public class SubmissionOptionField : AuditableEntity
{
    public int SubmissionId { get; private set; }
    
    public int ElementId { get; private set; }
    
    public virtual FormSubmission Submission { get; private set; } = null!;
    
    public virtual List<SubmissionOptionFieldValue> Values { get; private set; } = new();

    public SubmissionOptionField(int submissionId, int elementId)
    {
        SubmissionId = submissionId;
        ElementId = elementId;
    }

    private SubmissionOptionField()
    {
        
    }
}