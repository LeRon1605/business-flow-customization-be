using System.Linq.Expressions;
using BuildingBlocks.Domain.Specifications;
using Submission.Domain.SubmissionAggregate.Entities;

namespace Submission.Domain.SubmissionAggregate.Specifications;

public class SubmissionBySpaceSpecification : Specification<FormSubmission>
{
    private readonly int _spaceId;
    
    public SubmissionBySpaceSpecification(int spaceId)
    {
        _spaceId = spaceId;
    }
    
    public override Expression<Func<FormSubmission, bool>> ToExpression()
    {
        return x => x.FormVersion.Form.SpaceId == _spaceId;
    }
}