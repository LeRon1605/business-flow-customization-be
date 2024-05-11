using BuildingBlocks.Domain.Models;

namespace Submission.Domain.SubmissionAggregate.Entities;

public class SubmissionOptionFieldValue : Entity
{
    public int SubmissionFieldId { get; private set; }
    
    public int OptionId { get; private set; }

    public virtual SubmissionOptionField Field { get; private set; } = null!;
    
    public SubmissionOptionFieldValue(int optionId)
    {
        OptionId = optionId;
    }
    
    private SubmissionOptionFieldValue()
    {
        
    }
}