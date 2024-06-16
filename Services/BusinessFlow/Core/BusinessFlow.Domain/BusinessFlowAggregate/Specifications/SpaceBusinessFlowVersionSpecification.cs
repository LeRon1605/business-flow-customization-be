using System.Linq.Expressions;
using BuildingBlocks.Domain.Specifications;
using BusinessFlow.Domain.BusinessFlowAggregate.Entities;

namespace BusinessFlow.Domain.BusinessFlowAggregate.Specifications;

public class SpaceBusinessFlowVersionSpecification : Specification<BusinessFlowVersion, int>
{
    private readonly int _spaceId;
    
    public SpaceBusinessFlowVersionSpecification(int spaceId)
    {
        _spaceId = spaceId;
    }
    
    public override Expression<Func<BusinessFlowVersion, bool>> ToExpression()
    {
        return x => x.SpaceId == _spaceId;
    }
}