namespace BuildingBlocks.Domain.Models.Interfaces;

public interface IFullAuditableTenantAggregateRoot<TKey> : IAuditableTenantAggregateRoot<TKey>, IHasSoftDelete
    where TKey : IEquatable<TKey>
{
}

public interface IFullAuditableTenantAggregateRoot : IFullAuditableTenantAggregateRoot<int>
{
    
}

