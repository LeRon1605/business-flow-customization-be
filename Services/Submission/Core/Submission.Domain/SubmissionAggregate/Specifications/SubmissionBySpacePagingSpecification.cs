using System.Linq.Expressions;
using BuildingBlocks.Domain.Specifications;
using LinqKit;
using Submission.Domain.SubmissionAggregate.Entities;

namespace Submission.Domain.SubmissionAggregate.Specifications;

public class SubmissionBySpacePagingSpecification : PagingAndSortingSpecification<FormSubmission>
{
    private readonly int _spaceId;
    private readonly string? _search;
    
    public SubmissionBySpacePagingSpecification(int page, int size, string? sorting, int spaceId, string? search) : base(page, size, sorting)
    {
        _spaceId = spaceId;
        _search = search;
    }
    
    public override Expression<Func<FormSubmission, bool>> ToExpression()
    {
        var predicate = PredicateBuilder.New<FormSubmission>(x => x.FormVersion.Form.SpaceId == _spaceId);
        if (!string.IsNullOrWhiteSpace(_search))
        {
            predicate = predicate.And(x => x.Name.Contains(_search));    
        }

        return predicate;
    }
}