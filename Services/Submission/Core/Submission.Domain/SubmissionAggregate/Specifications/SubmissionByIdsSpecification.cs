using System.Linq.Expressions;
using BuildingBlocks.Domain.Specifications;
using Submission.Domain.SubmissionAggregate.Entities;

namespace Submission.Domain.SubmissionAggregate.Specifications;

public class SubmissionByIdsSpecification : Specification<FormSubmission>
{
    private readonly List<int> _ids;
    
    public SubmissionByIdsSpecification(List<int> ids)
    {
        _ids = ids;
    }
    
    public override Expression<Func<FormSubmission, bool>> ToExpression()
    {
        return x => _ids.Contains(x.Id);
    }
}