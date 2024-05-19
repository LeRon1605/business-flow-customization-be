using System.Linq.Expressions;
using BuildingBlocks.Domain.Specifications;
using BusinessFlow.Domain.SubmissionExecutionAggregate.Entities;
using BusinessFlow.Domain.SubmissionExecutionAggregate.Enums;

namespace BusinessFlow.Domain.SubmissionExecutionAggregate.Specifications;

public class SubmissionExecutionByStatusSpecification : Specification<SubmissionExecution>
{
    private readonly SubmissionExecutionStatus _status;
    
    public SubmissionExecutionByStatusSpecification(SubmissionExecutionStatus status)
    {
        _status = status;
    }
    
    public override Expression<Func<SubmissionExecution, bool>> ToExpression()
    {
        return x => x.Status == _status;
    }
}