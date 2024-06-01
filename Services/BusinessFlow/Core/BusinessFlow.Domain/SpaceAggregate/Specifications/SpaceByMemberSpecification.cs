using System.Linq.Expressions;
using BuildingBlocks.Domain.Specifications;
using BusinessFlow.Domain.SpaceAggregate.Entities;

namespace BusinessFlow.Domain.SpaceAggregate.Specifications;

public class SpaceByMemberSpecification : Specification<Space>
{
    private readonly string _memberId;
    
    public SpaceByMemberSpecification(string memberId)
    {
        _memberId = memberId;
    }
    
    public override Expression<Func<Space, bool>> ToExpression()
    {
        return x => x.Members.Any(m => m.UserId == _memberId);
    }
}