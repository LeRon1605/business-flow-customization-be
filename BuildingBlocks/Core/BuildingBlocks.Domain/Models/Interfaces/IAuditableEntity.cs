namespace BuildingBlocks.Domain.Models.Interfaces;

public interface IAuditableEntity<out TKey> : IEntity<TKey>, IHasAuditable where TKey : IEquatable<TKey>
{
}

public interface IAuditableEntity : IAuditableEntity<int>
{
}