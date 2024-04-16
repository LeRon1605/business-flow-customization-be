using System.Linq.Expressions;
using BuildingBlocks.Domain.Specifications;
using Identity.Domain.PermissionAggregate.Entities;

namespace Identity.Domain.PermissionAggregate.Specifications;

public class ReadOnlyPermissionSpecification : Specification<ApplicationPermission, int>
{
    public override Expression<Func<ApplicationPermission, bool>> ToExpression()
    {
        return x => x.Name.Contains("View");
    }
}