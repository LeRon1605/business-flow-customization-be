using BuildingBlocks.Domain.Models.Interfaces;
using BuildingBlocks.Domain.Repositories;
using BuildingBlocks.Domain.Specifications.Interfaces;
using BuildingBlocks.Infrastructure.EfCore.Common;
using Microsoft.EntityFrameworkCore;

namespace BuildingBlocks.Infrastructure.EfCore.Repositories;

public class EfCoreSpecificationRepository<TEntity, TKey> : ISpecificationRepository<TEntity, TKey>
    where TEntity : class, IEntity<TKey>
    where TKey : IEquatable<TKey>
{
    protected readonly DbContext DbContext;
    protected readonly DbSet<TEntity> DbSet;
    
    public EfCoreSpecificationRepository(DbContextFactory dbContextFactory)
    {
        DbContext = dbContextFactory.DbContext;
        DbSet = DbContext.Set<TEntity>();
    }

    public async Task<IList<TEntity>> FilterAsync(ISpecification<TEntity, TKey> specification)
    {
        return await GetQueryable(specification).ToListAsync();
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
        return await GetQueryable(specification).Select(projection.GetProject()).ToListAsync();
    }

    public Task<TEntity?> FindAsync(ISpecification<TEntity, TKey> specification)
    {
        return GetQueryable(specification).FirstOrDefaultAsync();
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
        return SpecificationEvaluator<TEntity, TKey>.GetQuery(DbSet.AsQueryable(), specification);
    }
    
    protected virtual IQueryable<TEntity> GetQueryable(IPagingAndSortingSpecification<TEntity, TKey> specification)
    {
        return SpecificationEvaluator<TEntity, TKey>.GetQuery(DbSet.AsQueryable(), specification);
    }
}

public class EfCoreSpecificationRepository<TEntity> : EfCoreSpecificationRepository<TEntity, int>
    where TEntity : class, IEntity
{
    public EfCoreSpecificationRepository(DbContextFactory dbContextFactory) : base(dbContextFactory)
    {
    }
}