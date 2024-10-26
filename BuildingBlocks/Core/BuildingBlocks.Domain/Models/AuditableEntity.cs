﻿using BuildingBlocks.Domain.Models.Interfaces;

namespace BuildingBlocks.Domain.Models;

public abstract class AuditableEntity<TKey> : Entity<TKey>, IAuditableEntity<TKey> where TKey : IEquatable<TKey>
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

public abstract class AuditableEntity : AuditableEntity<int>, IAuditableEntity
{
    
}