﻿using BuildingBlocks.Domain.Models;

namespace Identity.Domain.PermissionAggregate.Entities;

public class ApplicationPermission : AggregateRoot
{
    public string Name { get; private set; }
    public string Description { get; private set; }

    public ApplicationPermission(string name, string description)
    {
        Name = Guard.NotNullOrEmpty(name, nameof(Name));
        Description = Guard.NotNullOrEmpty(description, nameof(Description));
    }

    public void SetName(string name)
    {
        Name = Guard.NotNullOrEmpty(name, nameof(Name));
    }
    
    public void SetDescription(string description)
    {
        Description = Guard.NotNullOrEmpty(description, nameof(Description));
    }

    private ApplicationPermission()
    {
        
    }
}