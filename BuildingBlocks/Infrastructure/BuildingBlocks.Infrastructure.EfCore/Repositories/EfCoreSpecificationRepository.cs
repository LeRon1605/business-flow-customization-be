using BuildingBlocks.Application.Identity;
using BuildingBlocks.Domain.Models.Interfaces;
using BuildingBlocks.Domain.Repositories;
using BuildingBlocks.Domain.Specifications.Interfaces;
using BuildingBlocks.Infrastructure.EfCore.Common;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using LinqKit;

namespace BuildingBlocks.Infrastructure.EfCore.Repositories;

public class EfCoreSpecificationRepository<TEntity, TKey> : ISpecificationRepository<TEntity, TKey>
    where TEntity : class, IEntity<TKey>
    where TKey : IEquatable<TKey>
{
    protected readonly DbContext DbContext;
    protected readonly ICurrentUser CurrentUser;
    protected readonly DbSet<TEntity> DbSet;
    
    public EfCoreSpecificationRepository(DbContextFactory dbContextFactory, ICurrentUser currentUser)
    {
        DbContext = dbContextFactory.DbContext;
        CurrentUser = currentUser;
        DbSet = DbContext.Set<TEntity>();
    }

    public IQueryable<TEntity> GetQuery(ISpecification<TEntity, TKey> specification)
    {
        return GetQueryable(specification);
    }
    
    public async Task<IList<TEntity>> FilterAsync(ISpecification<TEntity, TKey> specification)
    {
        return await GetQueryable(specification).ToListAsync();
    }

    public async Task<IList<TOut>> FilterAsync<TOut>(ISpecification<TEntity, TKey> specification, IProjection<TEntity, TKey, TOut> projection)
    {
        return await GetQueryable(specification).Select(projection.GetProject().Expand()).ToListAsync();
    }

    public async Task<IList<TOut>> FilterAsync<TOut>(ISpecification<TEntity, TKey> specification, Expression<Func<TEntity, TOut>> projection)
    {
        return await GetQueryable(specification).Select(projection).ToListAsync();
    }

    public async Task<IList<TEntity>> GetPagedListAsync(int skip, int take, ISpecification<TEntity, TKey> specification, string? sorting = null)
    {
        return await GetQueryable(specification)
            .Skip(skip).Take(take).ToListAsync();
    }

    public async Task<IList<TEntity>> GetPagedListAsync(IPagingAndSortingSpecification<TEntity, TKey> specification)
    {
        return await GetQueryable(specification).ToListAsync();
    }

    public async Task<IList<TOut>> GetPagedListAsync<TOut>(IPagingAndSortingSpecification<TEntity, TKey> specification, IProjection<TEntity, TKey, TOut> projection)
    {
        return await GetQueryable(specification).Select(projection.GetProject().Expand()).ToListAsync();
    }

    public Task<TEntity> FindAsync(ISpecification<TEntity, TKey> specification)
    {
        return GetQueryable(specification).FirstOrDefaultAsync();
    }

    public Task<TOut> FindAsync<TOut>(ISpecification<TEntity, TKey> specification,
        IProjection<TEntity, TKey, TOut> projection)
    {
        return GetQueryable(specification).Select(projection.GetProject().Expand()).FirstOrDefaultAsync();
    }

    public Task<int> GetCountAsync(ISpecification<TEntity, TKey> specification)
    {
        return GetQueryable(specification).CountAsync(specification.ToExpression());
    }

    public Task<bool> AnyAsync(ISpecification<TEntity, TKey> specification)
    {
        return GetQueryable(specification).AnyAsync(specification.ToExpression());
    }

    public Task<bool> AllAsync(ISpecification<TEntity, TKey> specification)
    {
        return GetQueryable(specification).AllAsync(specification.ToExpression());
    }
    
    protected virtual IQueryable<TEntity> GetQueryable(ISpecification<TEntity, TKey> specification)
    {
        return SpecificationEvaluator<TEntity, TKey>.GetQuery(GetBaseQuery(), specification);
    }
    
    protected virtual IQueryable<TEntity> GetQueryable(IPagingAndSortingSpecification<TEntity, TKey> specification)
    {
        return SpecificationEvaluator<TEntity, TKey>.GetQuery(GetBaseQuery(), specification);
    }
    
    protected virtual IQueryable<TEntity> GetBaseQuery()
    {
        var queryable = DbSet.AsQueryable();

        if (typeof(TEntity).IsAssignableTo(typeof(ITenantEntity<TKey>)) && CurrentUser.IsAuthenticated)
        {
            queryable = queryable.Where(x => ((ITenantEntity<TKey>)x).TenantId == CurrentUser.TenantId);
        }
        
        return queryable;
    }
}

public class EfCoreSpecificationRepository<TEntity> : EfCoreSpecificationRepository<TEntity, int>
    where TEntity : class, IEntity
{
    public EfCoreSpecificationRepository(DbContextFactory dbContextFactory, ICurrentUser currentUser) : base(dbContextFactory, currentUser)
    {
    }
}