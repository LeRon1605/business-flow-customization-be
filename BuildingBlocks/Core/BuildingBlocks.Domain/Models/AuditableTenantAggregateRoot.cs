using BuildingBlocks.Domain.Models.Interfaces;

namespace BuildingBlocks.Domain.Models;

public class AuditableTenantAggregateRoot<TKey> : AuditableAggregateRoot<TKey>, IAuditableTenantEntity<TKey>
    where TKey : IEquatable<TKey>
{
    public int TenantId { get; set; }
}

public class AuditableTenantAggregateRoot : AuditableAggregateRoot, IAuditableTenantEntity
{
    public int TenantId { get; set; }
}