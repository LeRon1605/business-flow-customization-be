using System.Linq.Expressions;
using BuildingBlocks.Domain.Specifications;
using Identity.Domain.PermissionAggregate.Entities;

namespace Identity.Domain.PermissionAggregate.Specifications;

public class PermissionByNameSpecification : Specification<ApplicationPermission, int>
{
    private readonly string _name;
    
    public PermissionByNameSpecification(string name)
    {
        _name = name;
    }

    public override Expression<Func<ApplicationPermission, bool>> ToExpression()
    {
        return x => x.Name.Contains(_name);
    }
}