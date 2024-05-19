using BuildingBlocks.Domain.Models;
using Submission.Domain.FormAggregate.Entities;
using Submission.Domain.SubmissionAggregate.Abstracts;

namespace Submission.Domain.SubmissionAggregate.Entities;

public class SubmissionNumberValue : AuditableEntity, ISubmissionField<SubmissionNumberValue>
{
    public int SubmissionId { get; set; }
    
    public int ElementId { get; set; }

    public decimal? Value { get; private set; }
    
    public virtual FormElement Element { get; private set; } = null!;
    
    public virtual FormSubmission Submission { get; private set; } = null!;
    
    public SubmissionNumberValue(int elementId)
    {
        ElementId = elementId;
    }
    
    public SubmissionNumberValue(int elementId, decimal? value) : this(elementId)
    {
        Value = value;
    }
    
    public void UpdateValue(SubmissionNumberValue field)
    {
        Value = field.Value;
    }
    
    private SubmissionNumberValue()
    {
        
    }
}