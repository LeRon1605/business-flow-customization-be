using System.Linq.Expressions;
using BuildingBlocks.Domain.Specifications;
using LinqKit;
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
        var predicate = PredicateBuilder.New<ApplicationUser>(x => true);
        if (!string.IsNullOrWhiteSpace(_search))
        {
            predicate = predicate.And(x => x.UserName!.ToLower().Contains(_search.ToLower()) 
                                           || x.Email!.ToLower().Contains(_search.ToLower()));
        }
        
        return predicate;
    }
}