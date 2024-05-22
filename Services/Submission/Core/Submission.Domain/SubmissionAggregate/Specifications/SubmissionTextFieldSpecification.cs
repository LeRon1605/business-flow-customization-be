using System.Linq.Expressions;
using BuildingBlocks.Domain.Specifications;
using Submission.Domain.SubmissionAggregate.Entities;

namespace Submission.Domain.SubmissionAggregate.Specifications;

public class SubmissionTextFieldSpecification : Specification<FormSubmission, int>
{
    private readonly int _elementId;
    private readonly string _value;
    
    public SubmissionTextFieldSpecification(int elementId, string value)
    {
        _elementId = elementId;
        _value = value;
    }
    
    public override Expression<Func<FormSubmission, bool>> ToExpression()
    {
        return x => x.TextFields.Any(f => f.ElementId == _elementId 
                                          && !string.IsNullOrWhiteSpace(f.Value)
                                          && f.Value.Contains(_value));
    }
}