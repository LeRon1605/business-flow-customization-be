using BuildingBlocks.Domain.Models.Interfaces;

namespace BuildingBlocks.Domain.Models;

public class AuditableTenantEntity<TKey> : AuditableEntity<TKey>, IAuditableTenantEntity<TKey>
    where TKey : IEquatable<TKey>
{
    public int TenantId { get; set; }
}

public class AuditableTenantEntity : AuditableEntity, IAuditableTenantEntity
{
    public int TenantId { get; set; }
}