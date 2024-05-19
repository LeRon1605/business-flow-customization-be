using System.Linq.Expressions;
using BuildingBlocks.Domain.Specifications;
using Submission.Domain.SubmissionAggregate.Entities;

namespace Submission.Domain.SubmissionAggregate.Specifications;

public class SubmissionByExecutionSpecification : Specification<FormSubmission>
{
    private readonly int _executionId;
    
    public SubmissionByExecutionSpecification(int executionId)
    {
        _executionId = executionId;
    }
    
    public override Expression<Func<FormSubmission, bool>> ToExpression()
    {
        return x => x.ExecutionId.HasValue && x.ExecutionId == _executionId;
    }
}