namespace BuildingBlocks.Domain.Models.Interfaces;

public interface IFullAuditableAggregateRoot<out TKey> : IAuditableAggregateRoot<TKey>, IHasSoftDelete
    where TKey : IEquatable<TKey>
{
    
}

public interface IFullAuditableAggregateRoot : IFullAuditableAggregateRoot<int>
{
    
}
