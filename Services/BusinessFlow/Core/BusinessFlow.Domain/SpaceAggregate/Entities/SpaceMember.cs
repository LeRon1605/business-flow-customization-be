using BuildingBlocks.Domain.Models;

namespace BusinessFlow.Domain.SpaceAggregate.Entities;

public record SpaceMember : ValueObject
{
    public string UserId { get; private set; }
    
    public int SpaceId { get; private set; }
    
    public int RoleId { get; private set; }

    public virtual Space Space { get; private set; } = null!;
    
    public virtual SpaceRole Role { get; private set; } = null!;
    
    public SpaceMember(string userId, Enums.SpaceRole role)
    {
        UserId = userId;
        RoleId = (int)role;
    }
    
    public void UpdateRole(Enums.SpaceRole role)
    {
        RoleId = (int)role;
    }
    
    private SpaceMember()
    {
        
    }
}