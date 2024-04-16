using BuildingBlocks.Domain.Models.Interfaces;

namespace BuildingBlocks.Domain.Models;

public class TenantAggregateRoot<TKey> : AggregateRoot<TKey>, ITenantAggregateRoot<TKey> where TKey : IEquatable<TKey>
{
    public int TenantId { get; set; }
}

public class TenantAggregateRoot : AggregateRoot, ITenantAggregateRoot
{
    public int TenantId { get; set; }
}