using System.Linq.Expressions;
using BuildingBlocks.Domain.Specifications;
using BuildingBlocks.Kernel.Models;
using LinqKit;
using Submission.Domain.SubmissionAggregate.Entities;

namespace Submission.Domain.SubmissionAggregate.Specifications;

public class SubmissionDateFieldSpecification : Specification<FormSubmission, int>
{
    private readonly int _elementId;
    private readonly List<TimeRange> _timeRanges;
    
    public SubmissionDateFieldSpecification(int elementId, List<TimeRange> timeRanges)
    {
        _elementId = elementId;
        _timeRanges = timeRanges;
    }
    
    public override Expression<Func<FormSubmission, bool>> ToExpression()
    {
        var predicate = PredicateBuilder.New<FormSubmission>(x => false);
        foreach (var timeRange in _timeRanges)
        {
            predicate = predicate.Or(x => x.DateFields.Any(f => f.ElementId == _elementId 
                                                               && timeRange.From <= f.Value 
                                                               && f.Value <= timeRange.To));
        }

        return predicate;
    }
}