using BuildingBlocks.Domain.Models.Interfaces;

namespace BuildingBlocks.Domain.Models;

public class FullAuditableTenantAggregateRoot<TKey> : AuditableTenantAggregateRoot<TKey>, IFullAuditableTenantAggregateRoot<TKey> where TKey : IEquatable<TKey>
{
    public bool IsDeleted { get; private set; }
    public string? DeletedBy { get; private set; }
    
    public void Delete(string deletedBy)
    {
        IsDeleted = true;
        DeletedBy = deletedBy;
        LastModified = DateTime.UtcNow;
    }
}

public class FullAuditableTenantAggregateRoot : FullAuditableTenantAggregateRoot<int>, IFullAuditableTenantAggregateRoot
{
}
