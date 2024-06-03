using System.Linq.Expressions;
using BuildingBlocks.Domain.Specifications;
using BusinessFlow.Domain.SubmissionExecutionAggregate.Entities;

namespace BusinessFlow.Domain.SubmissionExecutionAggregate.Specifications;

public class SubmissionExecutionByIncludedSubmissionIdsSpecification : Specification<SubmissionExecution>
{
    private readonly List<int> _submissionIds;
    
    public SubmissionExecutionByIncludedSubmissionIdsSpecification(List<int> submissionIds)
    {
        _submissionIds = submissionIds;
    }
    
    public override Expression<Func<SubmissionExecution, bool>> ToExpression()
    {
        return x => _submissionIds.Contains(x.SubmissionId);
    }
}