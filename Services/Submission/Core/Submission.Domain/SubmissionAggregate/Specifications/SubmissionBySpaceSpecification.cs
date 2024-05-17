using System.Linq.Expressions;
using BuildingBlocks.Domain.Specifications;
using Submission.Domain.SubmissionAggregate.Entities;

namespace Submission.Domain.SubmissionAggregate.Specifications;

public class SubmissionBySpaceSpecification : PagingAndSortingSpecification<FormSubmission>
{
    private readonly int _spaceId;
    
    public SubmissionBySpaceSpecification(int page, int size, string? sorting, int spaceId) : base(page, size, sorting)
    {
        _spaceId = spaceId;
    }
    
    public override Expression<Func<FormSubmission, bool>> ToExpression()
    {
        return x => x.FormVersion.Form.SpaceId == _spaceId;
    }
}