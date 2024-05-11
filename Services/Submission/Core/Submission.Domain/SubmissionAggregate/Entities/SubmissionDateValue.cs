using BuildingBlocks.Domain.Models;
using Submission.Domain.SubmissionAggregate.Abstracts;

namespace Submission.Domain.SubmissionAggregate.Entities;

public class SubmissionDateValue : AuditableEntity, ISubmissionField
{
    public int SubmissionId { get; set; }
    
    public int ElementId { get; set; }
    
    public DateTime? Value { get; private set; }

    public virtual FormSubmission Submission { get; private set; } = null!;
    
    public SubmissionDateValue(int elementId)
    {
        ElementId = elementId;
    }
    
    public SubmissionDateValue(int elementId, DateTime value) : this(elementId)
    {
        Value = value;
    }

    private SubmissionDateValue()
    {
        
    }
}