using BuildingBlocks.Domain.Models;

namespace Submission.Domain.FormAggregate.Entities;

public class FormVersion : AuditableTenantAggregateRoot
{
    public int SpaceId { get; set; }
    
    public int FormId { get; set; }
    
    public virtual Form Form { get; set; } = null!;
    
    public virtual List<FormElement> Elements { get; set; } = new();
    
    public FormVersion(int spaceId, int formId)
    {
        SpaceId = spaceId;
        FormId = formId;
    }
    
    private FormVersion()
    {
    }
}