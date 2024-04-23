using System.Linq.Expressions;
using BuildingBlocks.Domain.Specifications;
using ApplicationUser = Identity.Domain.UserAggregate.Entities.ApplicationUser;

namespace Identity.Domain.UserAggregate.Specifications;

public class UserByTenantSpecification: Specification<ApplicationUser, string>
{
    private readonly int _tenantId;
    
    public UserByTenantSpecification(int tenantId)
    {
        _tenantId = tenantId;
        
        AddInclude(x => x.Tenants!);
    }
    
    public override Expression<Func<ApplicationUser, bool>> ToExpression()
    {
        return x => x.Tenants!.Any(t => t.TenantId == _tenantId);
    }
}