using BuildingBlocks.Domain.Models.Interfaces;

namespace BuildingBlocks.Domain.Repositories;

public interface IReadOnlyRepository<TAggregateRoot, TKey> : IBasicReadOnlyRepository<TAggregateRoot, TKey> 
    where TAggregateRoot : class, IAggregateRoot<TKey>
    where TKey : IEquatable<TKey>
{
}

public interface IReadOnlyRepository<TAggregate> : IReadOnlyRepository<TAggregate, int>
    where TAggregate : class, IAggregateRoot
{
}