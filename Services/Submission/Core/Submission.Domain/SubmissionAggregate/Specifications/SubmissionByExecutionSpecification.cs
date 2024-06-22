using System.Linq.Expressions;
using BuildingBlocks.Domain.Specifications;
using Submission.Domain.SubmissionAggregate.Entities;

namespace Submission.Domain.SubmissionAggregate.Specifications;

public class SubmissionByExecutionSpecification : Specification<FormSubmission>
{
    private readonly List<int> _executionIds;
    
    public SubmissionByExecutionSpecification(int executionId)
    {
        _executionIds = new List<int>() { executionId };
    }
    
    public SubmissionByExecutionSpecification(List<int> executionIds)
    {
        _executionIds = executionIds;
    }
    
    public override Expression<Func<FormSubmission, bool>> ToExpression()
    {
        return x => x.ExecutionId.HasValue && _executionIds.Contains(x.ExecutionId.Value);
    }
}