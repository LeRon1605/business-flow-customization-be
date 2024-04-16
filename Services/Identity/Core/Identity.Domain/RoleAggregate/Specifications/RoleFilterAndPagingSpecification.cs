using System.Linq.Expressions;
using BuildingBlocks.Domain.Specifications;
using ApplicationRole = Identity.Domain.RoleAggregate.Entities.ApplicationRole;

namespace Identity.Domain.RoleAggregate.Specifications;

public class RoleFilterAndPagingSpecification : PagingAndSortingSpecification<ApplicationRole, string>
{
    private readonly string _name;
    
    public RoleFilterAndPagingSpecification(int page, int size, string? sorting, string name) 
        : base(page, size, sorting, false)
    {
        _name = name;
    }

    public override Expression<Func<ApplicationRole, bool>> ToExpression()
    {
        return x => x.Name.Contains(_name);
    }
}