using BuildingBlocks.Domain.Models.Interfaces;

namespace BuildingBlocks.Domain.Repositories;

public interface ICommandRepository<in TAggregate, TKey> : IBaseRepository
    where TAggregate : class, IAggregateRoot<TKey> 
    where TKey : IEquatable<TKey>
{
    Task InsertAsync(TAggregate entity);

    Task InsertRangeAsync(IEnumerable<TAggregate> entities);

    void Insert(TAggregate entity);

    void Delete(TAggregate entity);

    void Update(TAggregate entity);
}

public interface ICommandRepository<in TAggregate> : ICommandRepository<TAggregate, int>
    where TAggregate : class, IAggregateRoot
{
}