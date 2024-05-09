using BuildingBlocks.Domain.Models;

namespace Submission.Domain.SubmissionAggregate.Entities;

public class SubmissionNumberValue : AuditableEntity
{
    public int SubmissionId { get; private set; }   
    
    public int ElementId { get; private set; }
    
    public decimal Value { get; private set; }
    
    public virtual FormSubmission Submission { get; private set; } = null!;
    
    public SubmissionNumberValue(int submissionId, int elementId, decimal value)
    {
        SubmissionId = submissionId;
        ElementId = elementId;
        Value = value;
    }
    
    private SubmissionNumberValue()
    {
        
    }
}