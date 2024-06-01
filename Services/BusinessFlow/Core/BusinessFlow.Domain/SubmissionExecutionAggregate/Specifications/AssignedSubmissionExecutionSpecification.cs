using System.Linq.Expressions;
using BuildingBlocks.Domain.Specifications;
using BusinessFlow.Domain.SubmissionExecutionAggregate.Entities;

namespace BusinessFlow.Domain.SubmissionExecutionAggregate.Specifications;

public class AssignedSubmissionExecutionSpecification : Specification<SubmissionExecution, int>
{
    private readonly string _userId;
    
    public AssignedSubmissionExecutionSpecification(string userId)
    {
        _userId = userId;
    }
    
    public override Expression<Func<SubmissionExecution, bool>> ToExpression()
    {
        return x => x.PersonInCharges.Any(p => p.UserId == _userId);
    }
}