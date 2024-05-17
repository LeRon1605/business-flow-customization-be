using System.Linq.Expressions;
using BuildingBlocks.Domain.Specifications;
using Submission.Domain.SubmissionAggregate.Entities;

namespace Submission.Domain.SubmissionAggregate.Specifications;

public class SubmissionByVersionSpecification : Specification<FormSubmission>
{
    private readonly int _formVersionId;
    
    public SubmissionByVersionSpecification(int formVersionId)
    {
        _formVersionId = formVersionId;
    }
    
    public override Expression<Func<FormSubmission, bool>> ToExpression()
    {
        return x => x.FormVersionId == _formVersionId;
    }
}