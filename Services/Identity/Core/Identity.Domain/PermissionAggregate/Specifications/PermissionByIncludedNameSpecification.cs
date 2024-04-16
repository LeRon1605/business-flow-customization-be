using System.Linq.Expressions;
using BuildingBlocks.Domain.Specifications;
using Identity.Domain.PermissionAggregate.Entities;

namespace Identity.Domain.PermissionAggregate.Specifications;

public class PermissionByIncludedNameSpecification : Specification<ApplicationPermission, int>
{
    private readonly IEnumerable<string> _names;
    
    public PermissionByIncludedNameSpecification(IEnumerable<string> names)
    {
        _names = names;
    }

    public override Expression<Func<ApplicationPermission, bool>> ToExpression()
    {
        return x => _names.Contains(x.Name);
    }
}