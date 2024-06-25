using System.Linq.Expressions;
using BuildingBlocks.Domain.Specifications;
using BusinessFlow.Domain.SpaceAggregate.Entities;

namespace BusinessFlow.Domain.SpaceAggregate.Specifications;

public class SpacePermissionSpecification : Specification<SpacePermission, int>
{
    private readonly int _roleId;
    
    public SpacePermissionSpecification(int roleId)
    {
        _roleId = roleId;
    }
    
    public override Expression<Func<SpacePermission, bool>> ToExpression()
    {
        return p => p.RoleId == _roleId;
    }
}