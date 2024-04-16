using System.Linq.Expressions;
using BuildingBlocks.Domain.Specifications;
using ApplicationUser = Identity.Domain.UserAggregate.Entities.ApplicationUser;

namespace Identity.Domain.UserAggregate.Specifications;

public class FilterApplicationUserSpecification : PagingAndSortingSpecification<ApplicationUser, string>
{
    private readonly string? _search;

    public FilterApplicationUserSpecification(int page, int size, string? sorting, string? search) : base(page, size, sorting)
    {
        _search = search;
    }
    
    public override Expression<Func<ApplicationUser, bool>> ToExpression()
    {
        return x => string.IsNullOrWhiteSpace(_search) ||
                    x.UserName.ToLower().Contains(_search.ToLower()) ||
                    x.Email.ToLower().Contains(_search.ToLower());
    }
}