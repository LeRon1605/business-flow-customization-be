using System.Data.Entity;
using System.Linq.Expressions;
using BuildingBlocks.Application.Identity;
using BuildingBlocks.Domain.Models.Interfaces;
using BuildingBlocks.Domain.Repositories;
using BuildingBlocks.Infrastructure.MongoDb.Common;
using LinqKit;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace BuildingBlocks.Infrastructure.MongoDb.Repositories;

public class MongoDbBasicReadOnlyRepository<TEntity, TKey> : MongoDbSpecificationRepository<TEntity, TKey>, IBasicReadOnlyRepository<TEntity, TKey>
    where TEntity : class, IEntity<TKey>
    where TKey : IEquatable<TKey>

{
    public MongoDbBasicReadOnlyRepository(IMongoDatabase database, ICurrentUser currentUser) : base(database, currentUser)
    {
    }

    public IQueryable<TEntity> GetQuery()
    {
        return GetBaseQuery();
    }

    public async Task<IList<TEntity>> FindByIncludedIdsAsync(IEnumerable<TKey> ids)
    {
        return await GetQueryable().Where(x => ids.Contains(x.Id)).ToListAsync();
    }

    public async Task<IList<TOut>> FindByIncludedIdsAsync<TOut>(IEnumerable<TKey> ids, IProjection<TEntity, TKey, TOut> projection)
    {
        return await GetQueryable().Where(x => ids.Contains(x.Id)).Select(projection.GetProject()).ToListAsync();
    }

    public async Task<IList<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>>? expression = null, string? sorting = null)
    {
        return await new AppQueryableBuilder<TEntity, TKey>(GetQueryable())
                                    .ApplyFilter(expression)
                                    .ApplySorting(sorting)
                                    .Build()
                                    .ToListAsync();
    }

    public async Task<IList<TOut>> FindAllAsync<TOut>(IProjection<TEntity, TKey, TOut> projection, Expression<Func<TEntity, bool>>? expression = null, string? sorting = null)
    {
        return await new AppQueryableBuilder<TEntity, TKey>(GetQueryable())
            .ApplyFilter(expression)
            .ApplySorting(sorting)
            .Build()
            .Select(projection.GetProject().Expand())
            .ToListAsync();
    }

    public async Task<IList<TEntity>> GetPagedListAsync(int skip
        , int take
        , Expression<Func<TEntity, bool>> expression
        , string? sorting = null
        , bool tracking = true
        , string? includeProps = null)
    {
        var queryable = new AppQueryableBuilder<TEntity, TKey>(GetQueryable(tracking))
                                    .IncludeProp(includeProps)
                                    .ApplyFilter(expression)
                                    .ApplySorting(sorting)
                                    .Build();

        return await queryable.Skip(skip).Take(take).ToListAsync();
    }

    public async Task<TEntity?> FindByIdAsync(object id, string? includeProps = null, bool tracking = true)
    {
        var queryable = new AppQueryableBuilder<TEntity, TKey>(GetQueryable(tracking))
                                    .IncludeProp(includeProps)
                                    .Build();

        return await queryable.FirstOrDefaultAsync(x => id.Equals(x.Id));
    }

    public async Task<TOut?> FindByIdAsync<TOut>(object id, IProjection<TEntity, TKey, TOut> projection, string? includeProps = null, bool tracking = true)
    {
        var queryable = new AppQueryableBuilder<TEntity, TKey>(GetQueryable(tracking))
            .IncludeProp(includeProps)
            .Build();

        return await queryable
            .Where(x => id.Equals(x.Id))
            .Select(projection.GetProject().Expand())
            .FirstOrDefaultAsync();
    }

    public async Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> expression, bool tracking = true,
        string? includeProps = null)
    {
        var queryable = new AppQueryableBuilder<TEntity, TKey>(GetQueryable(tracking))
                                    .IncludeProp(includeProps)
                                    .Build();
        
        return await queryable.FirstOrDefaultAsync(expression);
    }

    public Task<bool> IsExistingAsync(TKey id)
    {
        var queryable = GetQueryable();
        return queryable.AnyAsync(x => id.Equals(x.Id));
    }

    public Task<bool> AnyAsync(Expression<Func<TEntity, bool>>? expression = null)
    {
        var queryable = GetQueryable();
        return expression != null ? queryable.AnyAsync(expression) : queryable.AnyAsync();
    }

    public Task<bool> AllAsync(Expression<Func<TEntity, bool>> expression)
    {
        var queryable = GetQueryable();
        return queryable.AllAsync(expression);
    }

    public Task<int> GetCountAsync(Expression<Func<TEntity, bool>>? expression = null)
    {
        var queryable = GetQueryable();
        return expression != null ? queryable.CountAsync(expression) : queryable.CountAsync();
    }

    public Task<decimal> GetAverageAsync(Expression<Func<TEntity, decimal>> selector, Expression<Func<TEntity, bool>>? expression = null)
    {
        var queryable = GetQueryable();
        if (expression != null)
        {
            queryable = queryable.Where(expression);
        }

        return queryable.AverageAsync(selector);
    }
    
    protected virtual IMongoQueryable<TEntity> GetQueryable(bool tracking = true)
    {
        return new AppQueryableBuilder<TEntity, TKey>(GetBaseQuery()).Build();
    }
}