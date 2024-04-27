using System.Linq.Expressions;
using BuildingBlocks.Domain.Specifications;
using Identity.Domain.TenantAggregate.Entities;
using LinqKit;

namespace Identity.Domain.TenantAggregate.Specifications;

public class InvitationByTenantSpecification : PagingAndSortingSpecification<TenantInvitation, int>
{
    private readonly int _tenantId;
    private readonly string? _search;
    
    public InvitationByTenantSpecification(int page, int size, string? sorting, string? search, int tenantId) 
        : base(page, size, sorting)
    {
        _tenantId = tenantId;
        _search = search;
    }
    
    public override Expression<Func<TenantInvitation, bool>> ToExpression()
    {
        var predicate = PredicateBuilder.New<TenantInvitation>(_ => _.TenantId == _tenantId);
        if (!string.IsNullOrEmpty(_search))
            predicate = predicate.And(_ => _.Email.Contains(_search!));
        
        return predicate;
    }
}