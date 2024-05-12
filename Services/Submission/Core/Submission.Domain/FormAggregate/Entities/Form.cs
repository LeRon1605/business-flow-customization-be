using BuildingBlocks.Domain.Models;

namespace Submission.Domain.FormAggregate.Entities;

public class Form : AuditableTenantAggregateRoot
{
    public int SpaceId { get; set; }
    
    public string Name { get; private set; }
    
    public string CoverImageUrl { get; private set; }
    
    public virtual List<FormVersion> Versions { get; private set; } = new();
    
    public Form(int spaceId, string name, string coverImageUrl)
    {
        SpaceId = spaceId;
        Name = name;
        CoverImageUrl = coverImageUrl;
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
    
    private Form()
    {
    }
}