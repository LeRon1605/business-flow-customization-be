using System.Linq.Expressions;
using BuildingBlocks.Domain.Specifications;
using BusinessFlow.Domain.SpaceAggregate.Entities;

namespace BusinessFlow.Domain.SpaceAggregate.Specifications;

public class SpaceByTenantAndMemberSpecification : Specification<Space, int>
{
    private readonly string _memberId;
    
    public SpaceByTenantAndMemberSpecification(string memberId)
    {
        _memberId = memberId;
    }
    
    public override Expression<Func<Space, bool>> ToExpression()
    {
        return x => x.Members.Any(m => m.UserId == _memberId);
    }
}