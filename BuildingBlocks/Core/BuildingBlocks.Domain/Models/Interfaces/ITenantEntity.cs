namespace BuildingBlocks.Domain.Models.Interfaces;

public interface ITenantEntity<out TKey> : IEntity<TKey>, IHasTenant where TKey : IEquatable<TKey>
{
    
}

public interface ITenantEntity : ITenantEntity<int>, IEntity
{
}