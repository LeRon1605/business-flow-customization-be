using System.Linq.Expressions;
using BuildingBlocks.Domain.Specifications;
using Submission.Domain.SubmissionAggregate.Entities;

namespace Submission.Domain.SubmissionAggregate.Specifications;

public class SubmissionByCreatorSpecification : Specification<FormSubmission, int>
{
    private readonly List<string> _creatorIds;
    
    public SubmissionByCreatorSpecification(List<string> creatorIds)
    {
        _creatorIds = creatorIds;
    }
    
    public override Expression<Func<FormSubmission, bool>> ToExpression()
    {
        return x => !string.IsNullOrEmpty(x.CreatedBy) && _creatorIds.Contains(x.CreatedBy);
    }
}