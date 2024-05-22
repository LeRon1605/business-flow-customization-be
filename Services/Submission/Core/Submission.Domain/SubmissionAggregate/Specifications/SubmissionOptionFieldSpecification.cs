using System.Linq.Expressions;
using BuildingBlocks.Domain.Specifications;
using Submission.Domain.SubmissionAggregate.Entities;

namespace Submission.Domain.SubmissionAggregate.Specifications;

public class SubmissionOptionFieldSpecification : Specification<FormSubmission, int>
{
    private readonly int _elementId;
    private readonly int[] _optionIds;
    
    public SubmissionOptionFieldSpecification(int elementId, int[] optionIds)
    {
        _elementId = elementId;
        _optionIds = optionIds;
    }
    
    public override Expression<Func<FormSubmission, bool>> ToExpression()
    {
        if (!_optionIds.Any())
            return x => true;
        
        return x => x.OptionFields.Any(f => f.ElementId == _elementId 
                                            && f.Values.Any(v => _optionIds.Contains(v.OptionId)));
    }
}