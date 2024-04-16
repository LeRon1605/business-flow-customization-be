namespace BuildingBlocks.Domain.Models.Interfaces;

public interface IAuditableTenantEntity<out TKey> : IAuditableEntity<TKey>, ITenantEntity<TKey>
    where TKey : IEquatable<TKey>
{
    
}

public interface IAuditableTenantEntity : IAuditableTenantEntity<int>, IAuditableEntity, ITenantEntity
{
    
}