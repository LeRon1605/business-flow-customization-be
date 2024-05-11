using BuildingBlocks.Domain.Models;

namespace Submission.Domain.FormAggregate.Entities;

public class Form : AuditableTenantAggregateRoot
{
    public string Name { get; private set; }
    
    public string CoverImageUrl { get; private set; }
    
    public virtual List<FormVersion> Versions { get; private set; } = new();
    
    public Form(string name, string coverImageUrl)
    {
        Name = name;
        CoverImageUrl = coverImageUrl;
    }
    
    private Form()
    {
    }
}