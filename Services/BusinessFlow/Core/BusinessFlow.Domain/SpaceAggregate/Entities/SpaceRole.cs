using BuildingBlocks.Domain.Models;

namespace BusinessFlow.Domain.SpaceAggregate.Entities;

public class SpaceRole : AggregateRoot
{
    public string Name { get; private set; }

    public virtual List<SpaceMember> Members { get; private set; } = new();
    public virtual List<SpacePermission> Permissions { get; private set; } = new();
    
    public SpaceRole(string name)
    {
        Name = name;
    }
    
    private SpaceRole()
    {
        
    }
}