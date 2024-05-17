using System.Linq.Expressions;
using BuildingBlocks.Domain.Specifications;
using Submission.Domain.FormAggregate.Entities;

namespace Submission.Domain.FormAggregate.Specifications;

public class FormElementBySpaceSpecification : Specification<FormElement>
{
    private readonly int _spaceId;
    
    public FormElementBySpaceSpecification(int spaceId)
    {
        _spaceId = spaceId;
    }
    
    public override Expression<Func<FormElement, bool>> ToExpression()
    {
        return x => x.FormVersion.Form.SpaceId == _spaceId;
    }
}