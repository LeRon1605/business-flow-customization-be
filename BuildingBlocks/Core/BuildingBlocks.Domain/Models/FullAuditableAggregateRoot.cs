using BuildingBlocks.Domain.Models.Interfaces;

namespace BuildingBlocks.Domain.Models;

public abstract class FullAuditableAggregateRoot<TKey> : AuditableAggregateRoot<TKey>, IFullAuditableAggregateRoot<TKey> where TKey : IEquatable<TKey>
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

public abstract class FullAuditableAggregateRoot : FullAuditableAggregateRoot<int>, IFullAuditableAggregateRoot
{
    
}