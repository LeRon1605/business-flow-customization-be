using BuildingBlocks.Domain.Models;
using Submission.Domain.FormAggregate.Entities;
using Submission.Domain.SubmissionAggregate.Abstracts;

namespace Submission.Domain.SubmissionAggregate.Entities;

public class SubmissionOptionField : AuditableEntity, ISubmissionField
{
    public int SubmissionId { get; set; }
    
    public int ElementId { get; set; }
    
    public virtual FormElement Element { get; private set; } = null!;
    
    public virtual FormSubmission Submission { get; private set; } = null!;
    
    public virtual List<SubmissionOptionFieldValue> Values { get; private set; } = new();
    
    public SubmissionOptionField(int elementId)
    {
        ElementId = elementId;
    }

    public SubmissionOptionField(int elementId, int[] optionIds) : this(elementId)
    {
        AddSelectedOptions(optionIds);
    }
    
    public void AddSelectedOptions(int[] optionIds)
    {
        foreach (var optionId in optionIds)
        {
            var isExisted = Values.Any(x => x.OptionId == optionId);
            if (isExisted)
                continue;
            
            Values.Add(new SubmissionOptionFieldValue(optionId));
        }
    }


    private SubmissionOptionField()
    {
        
    }
}