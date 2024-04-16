using BuildingBlocks.Domain.Models.Interfaces;

namespace BuildingBlocks.Domain.Models;

public abstract class AggregateRoot<TKey> : Entity<TKey>, IAggregateRoot<TKey> where TKey : IEquatable<TKey>
{
    
}

public abstract class AggregateRoot : Entity, IAggregateRoot
{
    
}