using System.Linq.Expressions;
using BuildingBlocks.Domain.Specifications;
using Submission.Domain.SubmissionAggregate.Entities;

namespace Submission.Domain.SubmissionAggregate.Specifications;

public class SubmissionByIdSpecification : Specification<FormSubmission>
{
    private readonly int _submissionId;
    
    public SubmissionByIdSpecification(int submissionId)
    {
        _submissionId = submissionId;
    }
    
    public override Expression<Func<FormSubmission, bool>> ToExpression()
    {
        return x => x.Id == _submissionId;
    }
}