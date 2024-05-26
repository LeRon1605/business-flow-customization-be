using System.Linq.Expressions;
using BuildingBlocks.Domain.Models.Interfaces;
using BuildingBlocks.Kernel.Services;

namespace BuildingBlocks.Domain.Repositories;

public interface IBasicReadOnlyRepository<TEntity, TKey> : ISpecificationRepository<TEntity, TKey>
    where TEntity : IEntity<TKey>
    where TKey : IEquatable<TKey>
{
    IQueryable<TEntity> GetQuery();
    
    Task<IList<TEntity>> FindByIncludedIdsAsync(IEnumerable<TKey> ids);
    
    Task<IList<TOut>> FindByIncludedIdsAsync<TOut>(IEnumerable<TKey> ids, IProjection<TEntity, TKey, TOut> projection);
    
    Task<IList<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>>? expression = null, string? sorting = null);
    
    Task<IList<TOut>> FindAllAsync<TOut>(IProjection<TEntity, TKey, TOut> projection, Expression<Func<TEntity, bool>>? expression = null, string? sorting = null);

    Task<IList<TEntity>> GetPagedListAsync(int skip, int take, Expression<Func<TEntity, bool>> expression, string? sorting = null, bool tracking = true, string? includeProps = null);

    Task<TEntity?> FindByIdAsync(object id, string? includeProps = null, bool tracking = true);
    
    Task<TOut?> FindByIdAsync<TOut>(object id, IProjection<TEntity, TKey, TOut> projection, string? includeProps = null, bool tracking = true);

    Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> expression, bool tracking = true, string? includeProps = null);

    Task<bool> IsExistingAsync(TKey id);

    Task<bool> AnyAsync(Expression<Func<TEntity, bool>>? expression = null);
    
    Task<bool> AllAsync(Expression<Func<TEntity, bool>> expression);

    Task<int> GetCountAsync(Expression<Func<TEntity, bool>>? expression = null);

    Task<decimal> GetAverageAsync(Expression<Func<TEntity, decimal>> selector, Expression<Func<TEntity, bool>>? expression = null);
}