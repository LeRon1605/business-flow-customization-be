using System.Linq.Expressions;
using BuildingBlocks.Domain.Specifications;
using Identity.Domain.PermissionAggregate.Entities;

namespace Identity.Domain.PermissionAggregate.Specifications;

public class PermissionInSpecification : Specification<ApplicationPermission, int>
{
    private readonly IEnumerable<int> _permissionsIds;
    
    public PermissionInSpecification(IEnumerable<int> permissionsIds)
    {
        _permissionsIds = permissionsIds;
    }
    
    public override Expression<Func<ApplicationPermission, bool>> ToExpression()
    {
        return x => _permissionsIds.Contains(x.Id);
    }
}