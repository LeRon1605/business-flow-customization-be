using System.Linq.Expressions;
using BuildingBlocks.Domain.Specifications;
using Identity.Domain.UserAggregate.Entities;

namespace Identity.Domain.UserAggregate.Specifications;

public class UserByEmailSpecification : Specification<ApplicationUser, string>
{
    private readonly string _email;
    
    public UserByEmailSpecification(string email) 
    {
        _email = email;
    }
    
    public override Expression<Func<ApplicationUser, bool>> ToExpression()
    {
        return user => user.Email == _email;
    }
}