using BuildingBlocks.Domain.Models;
using BusinessFlow.Domain.BusinessFlowAggregate.Entities;

namespace BusinessFlow.Domain.SpaceAggregate.Entities;

public class Space : AuditableTenantAggregateRoot
{
    public string Name { get; private set; }
    
    public string Color { get; private set; }

    public virtual List<SpaceMember> Members { get; private set; } = new();
    
    public virtual List<BusinessFlowVersion> BusinessFlowVersions { get; private set; } = new();
    
    public Space(string name, string color)
    {
        Name = name;
        Color = color;
    }

    private Space()
    {
        
    }
}