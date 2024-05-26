using System.Data.Entity;
using BuildingBlocks.Application.Identity;
using BuildingBlocks.Domain.Models.Interfaces;
using BuildingBlocks.Domain.Repositories;
using BuildingBlocks.Domain.Specifications.Interfaces;
using BuildingBlocks.Infrastructure.MongoDb.Common;
using LinqKit;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace BuildingBlocks.Infrastructure.MongoDb.Repositories;

public class MongoDbSpecificationRepository<TEntity, TKey> : ISpecificationRepository<TEntity, TKey>
    where TEntity : class, IEntity<TKey>
    where TKey : IEquatable<TKey>
{
    protected readonly IMongoDatabase Database;
    protected readonly IMongoCollection<TEntity> Collection;
    protected readonly ICurrentUser CurrentUser;
    
    public MongoDbSpecificationRepository(IMongoDatabase database, ICurrentUser currentUser)
    {
        Database = database;
        Collection = database.GetCollection<TEntity>(typeof(TEntity).Name);
        CurrentUser = currentUser;
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

    public async Task<TEntity?> FindAsync(ISpecification<TEntity, TKey> specification)
    {
        return await GetQueryable(specification).FirstOrDefaultAsync();
    }

    public async Task<TOut?> FindAsync<TOut>(ISpecification<TEntity, TKey> specification,IProjection<TEntity, TKey, TOut> projection)
    {
        return await GetQueryable(specification).Select(projection.GetProject().Expand()).FirstOrDefaultAsync();
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
    
    protected virtual IMongoQueryable<TEntity> GetQueryable(ISpecification<TEntity, TKey> specification)
    {
        return SpecificationEvaluator<TEntity, TKey>.GetQuery(GetBaseQuery(), specification);
    }
    
    protected virtual IMongoQueryable<TEntity> GetQueryable(IPagingAndSortingSpecification<TEntity, TKey> specification)
    {
        return SpecificationEvaluator<TEntity, TKey>.GetQuery(GetBaseQuery(), specification);
    }
    
    protected virtual IMongoQueryable<TEntity> GetBaseQuery()
    {
        var queryable = Collection.AsQueryable();

        if (typeof(TEntity).IsAssignableTo(typeof(ITenantEntity<TKey>)) && CurrentUser.IsAuthenticated)
        {
            queryable = queryable
                .Where(x => ((ITenantEntity<TKey>)x).TenantId == CurrentUser.TenantId);
        }
        
        return queryable;
    }
}