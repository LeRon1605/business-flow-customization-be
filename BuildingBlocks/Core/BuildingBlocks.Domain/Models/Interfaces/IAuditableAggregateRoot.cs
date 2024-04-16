namespace BuildingBlocks.Domain.Models.Interfaces;

public interface IAuditableAggregateRoot<out TKey> : IAggregateRoot<TKey>, IHasAuditable
    where TKey : IEquatable<TKey>
{
    
}

public interface IAuditableAggregateRoot : IAuditableAggregateRoot<int>, IAggregateRoot, IHasAuditable
{
    
}