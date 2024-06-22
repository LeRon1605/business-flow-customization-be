using System.Linq.Expressions;
using BuildingBlocks.Domain.Specifications;
using BuildingBlocks.Kernel.Models;
using LinqKit;
using Submission.Domain.SubmissionAggregate.Entities;

namespace Submission.Domain.SubmissionAggregate.Specifications;

public class SubmissionByCreatedAtSpecification : Specification<FormSubmission, int>
{
    private readonly List<TimeRange> _timeRanges;
    
    public SubmissionByCreatedAtSpecification(List<TimeRange> timeRanges)
    {
        _timeRanges = timeRanges;
    }
    
    public override Expression<Func<FormSubmission, bool>> ToExpression()
    {
        var predicate = PredicateBuilder.New<FormSubmission>(x => false);
        foreach (var timeRange in _timeRanges)
        {
            predicate = predicate.Or(x => timeRange.From <= x.Created 
                                          && x.Created <= timeRange.To);
        }

        return predicate;
    }
}