using BuildingBlocks.Domain.Models.Interfaces;
using BuildingBlocks.Domain.Specifications.Interfaces;

namespace BuildingBlocks.Domain.Repositories;

public interface ISpecificationRepository<TEntity, TKey> : IBaseRepository
    where TEntity : IEntity<TKey>
    where TKey : IEquatable<TKey>
{
    IQueryable<TEntity> GetQuery(ISpecification<TEntity, TKey> specification);
    
    Task<IList<TEntity>> FilterAsync(ISpecification<TEntity, TKey> specification);
    
    Task<IList<TOut>> FilterAsync<TOut>(ISpecification<TEntity, TKey> specification, IProjection<TEntity, TKey, TOut> projection);
    
    Task<IList<TEntity>> GetPagedListAsync(int skip, int take, ISpecification<TEntity, TKey> specification, string? sorting = null);

    Task<IList<TEntity>> GetPagedListAsync(IPagingAndSortingSpecification<TEntity, TKey> specification);
    
    Task<IList<TOut>> GetPagedListAsync<TOut>(IPagingAndSortingSpecification<TEntity, TKey> specification, IProjection<TEntity, TKey, TOut> projection);

    Task<TEntity?> FindAsync(ISpecification<TEntity, TKey> specification);
    
    Task<TOut?> FindAsync<TOut>(ISpecification<TEntity, TKey> specification, IProjection<TEntity, TKey, TOut> projection);

    Task<int> GetCountAsync(ISpecification<TEntity, TKey> specification);

    Task<bool> AnyAsync(ISpecification<TEntity, TKey> specification);
    
    Task<bool> AllAsync(ISpecification<TEntity, TKey> specification);
}

public interface ISpecificationRepository<TEntity> : ISpecificationRepository<TEntity, int>
    where TEntity : IEntity
{
}