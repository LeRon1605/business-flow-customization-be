using System.Linq.Expressions;
using BuildingBlocks.Domain.Specifications;
using BusinessFlow.Domain.SpaceAggregate.Entities;

namespace BusinessFlow.Domain.SpaceAggregate.Specifications;

public class SpaceByTenantAndMemberSpecification : Specification<Space, int>
{
    private readonly string _memberId;
    private readonly int _tenantId;
    
    public SpaceByTenantAndMemberSpecification(string memberId, int tenantId)
    {
        _memberId = memberId;
        _tenantId = tenantId;
    }
    
    public override Expression<Func<Space, bool>> ToExpression()
    {
        return x => x.Members.Any(m => m.UserId == _memberId) && x.TenantId == _tenantId;
    }
}