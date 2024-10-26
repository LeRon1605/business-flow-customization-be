﻿using BuildingBlocks.Domain.Models.Interfaces;

namespace BuildingBlocks.Domain.Models;

public abstract class AuditableAggregateRoot<TKey> : AggregateRoot<TKey>, IAuditableAggregateRoot<TKey> where TKey : IEquatable<TKey>
{
    public DateTime Created { get; protected set; }
    public string? CreatedBy { get; protected set; }
    public DateTime? LastModified { get; protected set; }
    public string? LastModifiedBy { get; protected set; }
    
    public void Create(string createdBy)
    {
        Created = DateTime.UtcNow;
        CreatedBy = createdBy;
    }
    
    public void Update(string lastModifiedBy)
    {
        LastModified = DateTime.UtcNow;
        LastModifiedBy = lastModifiedBy;
    }
}

public abstract class AuditableAggregateRoot : AuditableAggregateRoot<int>, IAuditableAggregateRoot
{
    
}