﻿using BuildingBlocks.Domain.Models;

namespace Submission.Domain.FormAggregate.Entities;

public class Form : AuditableTenantAggregateRoot
{
    public int SpaceId { get; set; }
    
    public Guid? BusinessFlowBlockId { get; set; }
    
    public string Name { get; private set; }
    
    public string CoverImageUrl { get; private set; }
    
    public virtual List<FormVersion> Versions { get; private set; } = new();
    
    public bool IsShared { get; private set; }
    
    public string PublicToken { get; private set; }
    
    public Form(int spaceId, Guid? businessFlowBlockId, string name, string coverImageUrl)
    {
        SpaceId = spaceId;
        BusinessFlowBlockId = businessFlowBlockId;
        Name = name;
        CoverImageUrl = coverImageUrl;
        IsShared = false;
        PublicToken = Guid.NewGuid().ToString();
    }
    
    public void AddVersion(FormVersion formVersion)
    {
        Versions.Add(formVersion);
    }
    
    public void Update(string name, string coverImageUrl)
    {
        Name = name;
        CoverImageUrl = coverImageUrl;
    }

    public void Publish(bool status)
    {
        IsShared = status;
    }
    
    private Form()
    {
    }
}