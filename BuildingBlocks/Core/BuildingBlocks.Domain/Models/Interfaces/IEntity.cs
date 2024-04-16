namespace BuildingBlocks.Domain.Models.Interfaces;

public interface IEntity<out TKey> : IDomainModel where TKey : IEquatable<TKey>
{
    TKey Id { get; }
}

public interface IEntity : IEntity<int>
{
}