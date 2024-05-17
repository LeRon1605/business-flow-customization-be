using System.Linq.Expressions;
using BuildingBlocks.Domain.Specifications;
using Submission.Domain.FormAggregate.Entities;

namespace Submission.Domain.FormAggregate.Specifications;

public class FormElementByIncludedIdsSpecification : Specification<FormElement>
{
    private readonly IEnumerable<int> _includedIds;
    
    public FormElementByIncludedIdsSpecification(IEnumerable<int> includedIds)
    {
        _includedIds = includedIds;
    }
    
    public override Expression<Func<FormElement, bool>> ToExpression()
    {
        return x => _includedIds.Contains(x.Id);
    }
}