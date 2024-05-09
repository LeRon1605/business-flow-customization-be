using BuildingBlocks.Domain.Models;

namespace Submission.Domain.SubmissionAggregate.Entities;

public class SubmissionAttachmentField : AuditableEntity
{
    public int SubmissionId { get; private set; }
    
    public int ElementId { get; private set; }
    
    public virtual FormSubmission Submission { get; private set; } = null!;

    public virtual List<SubmissionAttachmentValue> Values { get; private set; } = new();
    
    public SubmissionAttachmentField(int submissionId, int elementId)
    {
        SubmissionId = submissionId;
        ElementId = elementId;
    }
    
    private SubmissionAttachmentField()
    {
        
    }
}