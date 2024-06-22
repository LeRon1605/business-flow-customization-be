using System.Linq.Expressions;
using BuildingBlocks.Domain.Specifications;
using Submission.Domain.SubmissionAggregate.Entities;

namespace Submission.Domain.SubmissionAggregate.Specifications;

public class SubmissionByExecutionBlockSpecification : Specification<FormSubmission>
{
    private readonly List<Guid> _businessFlowBlockIds;
    
    public SubmissionByExecutionBlockSpecification(List<Guid> businessFlowBlockIds)
    {
        _businessFlowBlockIds = businessFlowBlockIds;
    }
    
    public override Expression<Func<FormSubmission, bool>> ToExpression()
    {
        return x => x.Execution != null && _businessFlowBlockIds.Contains(x.Execution.BusinessFlowBlockId);
    }
}