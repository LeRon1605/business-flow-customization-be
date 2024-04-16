using BuildingBlocks.Domain.Models.Interfaces;

namespace BuildingBlocks.Domain.Models;

public class TenantEntity<TKey> : Entity<TKey>, ITenantEntity<TKey>
    where TKey : IEquatable<TKey>
{
    public int TenantId { get; set; }
}

public class TenantEntity : Entity, ITenantEntity
{
    public int TenantId { get; set; }
}