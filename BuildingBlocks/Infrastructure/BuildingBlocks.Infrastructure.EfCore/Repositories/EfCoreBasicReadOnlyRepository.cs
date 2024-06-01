﻿using System.Linq.Expressions;
using BuildingBlocks.Application.Identity;
using BuildingBlocks.Domain.Models.Interfaces;
using BuildingBlocks.Domain.Repositories;
using BuildingBlocks.Infrastructure.EfCore.Common;
using LinqKit;
using Microsoft.EntityFrameworkCore;

namespace BuildingBlocks.Infrastructure.EfCore.Repositories;

public class EfCoreBasicReadOnlyRepository<TEntity, TKey> : EfCoreSpecificationRepository<TEntity, TKey>, IBasicReadOnlyRepository<TEntity, TKey>
    where TEntity : class, IEntity<TKey>
    where TKey : IEquatable<TKey>
{
    public EfCoreBasicReadOnlyRepository(DbContextFactory dbContextFactory, ICurrentUser currentUser) : base(dbContextFactory, currentUser)
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

    public async Task<IList<TOut>> FindAllAsync<TOut>(Expression<Func<TEntity, TOut>> projection, Expression<Func<TEntity, bool>>? expression = null, string? sorting = null)
    {
        return await new AppQueryableBuilder<TEntity, TKey>(GetQueryable())
            .ApplyFilter(expression)
            .ApplySorting(sorting)
            .Build()
            .Select(projection)
            .ToListAsync();
    }

    public async Task<IList<TEntity>> GetPagedListAsync(int skip, int take, Expression<Func<TEntity, bool>> expression, string? sorting = null, bool tracking = true, string? includeProps = null)
    {
        var queryable = new AppQueryableBuilder<TEntity, TKey>(GetQueryable(tracking))
                                    .IncludeProp(includeProps)
                                    .ApplyFilter(expression)
                                    .ApplySorting(sorting)
                                    .Build();

        return await queryable.Skip(skip).Take(take).ToListAsync();
    }

    public Task<TEntity?> FindByIdAsync(object id, string? includeProps = null, bool tracking = true)
    {
        var queryable = new AppQueryableBuilder<TEntity, TKey>(GetQueryable(tracking))
                                    .IncludeProp(includeProps)
                                    .Build();

        return queryable.FirstOrDefaultAsync(x => id.Equals(x.Id));
    }

    public Task<TOut?> FindByIdAsync<TOut>(object id, IProjection<TEntity, TKey, TOut> projection, string? includeProps = null, bool tracking = true)
    {
        var queryable = new AppQueryableBuilder<TEntity, TKey>(GetQueryable(tracking))
            .IncludeProp(includeProps)
            .Build();

        return queryable.Where(x => id.Equals(x.Id)).Select(projection.GetProject().Expand()).FirstOrDefaultAsync();
    }

    public Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> expression, bool tracking = true,
        string? includeProps = null)
    {
        var queryable = new AppQueryableBuilder<TEntity, TKey>(GetQueryable(tracking))
                                    .IncludeProp(includeProps)
                                    .Build();
        
        return queryable.FirstOrDefaultAsync(expression);
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
    
    protected virtual IQueryable<TEntity> GetQueryable(bool tracking = true)
    {
        return new AppQueryableBuilder<TEntity, TKey>(GetBaseQuery(), false).Build();
    }
}

public class EfCoreBasicReadOnlyRepository<TEntity> : EfCoreBasicReadOnlyRepository<TEntity, Guid>
    where TEntity : class, IEntity<Guid>
{
    public EfCoreBasicReadOnlyRepository(DbContextFactory dbContextFactory, ICurrentUser currentUser) : base(dbContextFactory, currentUser)
    {
    }
}