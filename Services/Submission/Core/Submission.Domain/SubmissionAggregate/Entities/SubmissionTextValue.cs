using BuildingBlocks.Domain.Models;
using Submission.Domain.FormAggregate.Entities;
using Submission.Domain.SubmissionAggregate.Abstracts;

namespace Submission.Domain.SubmissionAggregate.Entities;

public class SubmissionTextValue : AuditableEntity, ISubmissionField<SubmissionTextValue>
{
    public int SubmissionId { get; set; }
    
    public int ElementId { get; set; }
    
    public string? Value { get; private set; }
    
    public virtual FormElement Element { get; private set; } = null!;
    
    public virtual FormSubmission Submission { get; private set; } = null!;
    
    public SubmissionTextValue(int elementId)
    {
        ElementId = elementId;
    }
    
    public SubmissionTextValue(int elementId, string value) : this(elementId)
    {
        Value = value;
    }
    
    public void UpdateValue(SubmissionTextValue field)
    {
        Value = field.Value;
    }

}