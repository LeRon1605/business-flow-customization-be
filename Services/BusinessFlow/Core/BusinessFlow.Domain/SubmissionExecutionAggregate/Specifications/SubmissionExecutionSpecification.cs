using System.Linq.Expressions;
using BuildingBlocks.Domain.Specifications;
using BusinessFlow.Domain.SubmissionExecutionAggregate.Entities;

namespace BusinessFlow.Domain.SubmissionExecutionAggregate.Specifications;

public class SubmissionExecutionSpecification : Specification<SubmissionExecution>
{
    private readonly int _submissionId;
    
    public SubmissionExecutionSpecification(int submissionId)
    {
        _submissionId = submissionId;
    }
    
    public override Expression<Func<SubmissionExecution, bool>> ToExpression()
    {
        return submissionExecution => submissionExecution.SubmissionId == _submissionId;
    }
}