using System.Linq.Expressions;
using BuildingBlocks.Domain.Specifications;
using Submission.Domain.SpaceBusinessFlowAggregate.Entities;

namespace Submission.Domain.SpaceBusinessFlowAggregate.Specifications;

public class SpaceBusinessFlowSpecification : Specification<SpaceBusinessFlow>
{
    private readonly int _spaceId;
    
    public SpaceBusinessFlowSpecification(int spaceId)
    {
        _spaceId = spaceId;
    }
    
    public override Expression<Func<SpaceBusinessFlow, bool>> ToExpression()
    {
        return x => x.SpaceId == _spaceId;
    }
}