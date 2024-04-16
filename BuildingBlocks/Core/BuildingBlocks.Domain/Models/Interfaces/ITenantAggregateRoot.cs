namespace BuildingBlocks.Domain.Models.Interfaces;

public interface ITenantAggregateRoot<TKey> : IAggregateRoot<TKey>, ITenantEntity<TKey> 
    where TKey : IEquatable<TKey>
{
    
}

public interface ITenantAggregateRoot : ITenantAggregateRoot<int>, IAggregateRoot, ITenantEntity
{
    
}