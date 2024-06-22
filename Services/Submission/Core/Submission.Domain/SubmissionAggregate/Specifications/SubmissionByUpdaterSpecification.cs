using System.Linq.Expressions;
using BuildingBlocks.Domain.Specifications;
using Submission.Domain.SubmissionAggregate.Entities;

namespace Submission.Domain.SubmissionAggregate.Specifications;

public class SubmissionByUpdaterSpecification : Specification<FormSubmission>
{
    private readonly List<string> _updaterIds;
    
    public SubmissionByUpdaterSpecification(List<string> updaterIds)
    {
        _updaterIds = updaterIds;
    }
    
    public override Expression<Func<FormSubmission, bool>> ToExpression()
    {
        return x => !string.IsNullOrEmpty(x.LastModifiedBy) && _updaterIds.Contains(x.LastModifiedBy);
    }
}