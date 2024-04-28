using BuildingBlocks.Domain.Models;

namespace BusinessFlow.Domain.SpaceAggregate.Entities;

public record SpaceMember : ValueObject
{
    public int UserId { get; private set; }
    
    public int SpaceId { get; private set; }
    
    public int RoleId { get; private set; }

    public virtual Space Space { get; private set; } = null!;
    
    public virtual SpaceRole Role { get; private set; } = null!;
    
    public SpaceMember(int userId, int spaceId, int roleId)
    {
        UserId = userId;
        SpaceId = spaceId;
        RoleId = roleId;
    }
    
    private SpaceMember()
    {
        
    }
}