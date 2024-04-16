using BuildingBlocks.Domain.Models.Interfaces;

namespace BuildingBlocks.Domain.Repositories;

public interface IRepository<TAggregateRoot, TKey> : IReadOnlyRepository<TAggregateRoot, TKey>, ICommandRepository<TAggregateRoot, TKey> 
    where TAggregateRoot : class, IAggregateRoot<TKey>
    where TKey : IEquatable<TKey>
{
    
}

public interface IRepository<TAggregateRoot> : IRepository<TAggregateRoot, int>
    where TAggregateRoot : class, IAggregateRoot
{
    
}