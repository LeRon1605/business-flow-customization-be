using System.Linq.Expressions;
using BuildingBlocks.Domain.Specifications;
using ApplicationUser = Identity.Domain.UserAggregate.Entities.ApplicationUser;

namespace Identity.Domain.UserAggregate.Specifications;

public class UserWithRoleSpecification : Specification<ApplicationUser, string>
{
    private readonly string _userId;
    
    public UserWithRoleSpecification(string userId)
    {
        _userId = userId;
    }
    
    public override Expression<Func<ApplicationUser, bool>> ToExpression()
    {
        return x => x.Id == _userId;
    }
}