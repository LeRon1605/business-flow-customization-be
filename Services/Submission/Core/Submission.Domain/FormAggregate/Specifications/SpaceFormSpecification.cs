using System.Linq.Expressions;
using BuildingBlocks.Domain.Specifications;
using Submission.Domain.FormAggregate.Entities;

namespace Submission.Domain.FormAggregate.Specifications;

public class SpaceFormSpecification : Specification<Form>
{
    private readonly int _spaceId;
    
    public SpaceFormSpecification(int spaceId)
    {
        _spaceId = spaceId;
    }
    
    public override Expression<Func<Form, bool>> ToExpression()
    {
        return x => x.SpaceId == _spaceId;
    }
}