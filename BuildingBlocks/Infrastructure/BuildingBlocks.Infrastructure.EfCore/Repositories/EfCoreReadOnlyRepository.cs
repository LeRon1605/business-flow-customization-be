using System.Linq.Expressions;
using BuildingBlocks.Domain.Models;
using BuildingBlocks.Domain.Models.Interfaces;
using BuildingBlocks.Domain.Repositories;
using BuildingBlocks.Domain.Specifications.Interfaces;
using BuildingBlocks.Infrastructure.EfCore.Common;
using Microsoft.EntityFrameworkCore;

namespace BuildingBlocks.Infrastructure.EfCore.Repositories;

public class EfCoreReadOnlyRepository<TAggregateRoot, TKey> : EfCoreBasicReadOnlyRepository<TAggregateRoot, TKey>, IReadOnlyRepository<TAggregateRoot, TKey>
    where TAggregateRoot : class, IAggregateRoot<TKey>
    where TKey : IEquatable<TKey>
{
    private readonly List<Expression<Func<TAggregateRoot, object>>> _defaultIncludeExpressions;
    private readonly List<string> _defaultIncludeStrings;
    
    public EfCoreReadOnlyRepository(DbContextFactory dbContextFactory) : base(dbContextFactory)
    {
        _defaultIncludeExpressions = new();
        _defaultIncludeStrings = new();
    }
    
    protected void AddInclude(Expression<Func<TAggregateRoot, object>> includeExpression)
    {
        ArgumentNullException.ThrowIfNull(includeExpression);
        
        _defaultIncludeExpressions.Add(includeExpression);
    }
    
    protected void AddInclude(string includeProp)
    {
        ArgumentException.ThrowIfNullOrEmpty(includeProp);
        
        _defaultIncludeStrings.Add(includeProp);
    }

    protected override IQueryable<TAggregateRoot> GetQueryable(bool tracking = true)
    {
        return new AppQueryableBuilder<TAggregateRoot, TKey>(DbSet, true)
            .IncludeProp(_defaultIncludeExpressions)
            .IncludeProp(_defaultIncludeStrings)
            .Build();
    }

    protected override IQueryable<TAggregateRoot> GetQueryable(ISpecification<TAggregateRoot, TKey> specification)
    {
        var specificationQueryable = SpecificationEvaluator<TAggregateRoot, TKey>.GetQuery(DbSet.AsQueryable(), specification);
        return new AppQueryableBuilder<TAggregateRoot, TKey>(specificationQueryable)
            .IncludeProp(_defaultIncludeStrings)
            .IncludeProp(_defaultIncludeExpressions)
            .Build();
    }
}

public class EfCoreReadOnlyRepository<TAggregateRoot> : EfCoreReadOnlyRepository<TAggregateRoot, int>
    where TAggregateRoot : AggregateRoot
{
    public EfCoreReadOnlyRepository(DbContextFactory dbContextFactory) : base(dbContextFactory)
    {
    }
}