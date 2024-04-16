namespace BuildingBlocks.Domain.Models.Interfaces;

public interface IAggregateRoot<out TKey> : IEntity<TKey> where TKey : IEquatable<TKey>
{
    
}

public interface IAggregateRoot : IAggregateRoot<int>
{
    
}