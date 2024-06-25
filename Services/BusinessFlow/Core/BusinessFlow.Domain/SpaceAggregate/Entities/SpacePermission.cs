using BuildingBlocks.Domain.Models;

namespace BusinessFlow.Domain.SpaceAggregate.Entities;

public class SpacePermission : AggregateRoot
{
    public int RoleId { get; set; }
    public string Name { get; set; }
    public virtual SpaceRole Role { get; private set; } = null!;

    public SpacePermission(int roleId, string name)
    {
        RoleId = roleId;
        Name = name;
    }

    private SpacePermission()
    {
        
    }
}