using BuildingBlocks.Domain.Models;
using BusinessFlow.Domain.BusinessFlowAggregate.Entities;
using BusinessFlow.Domain.SpaceAggregate.Exceptions;

namespace BusinessFlow.Domain.SpaceAggregate.Entities;

public class Space : AuditableTenantAggregateRoot
{
    public string Name { get; private set; }
    
    public string Description { get; private set; }
    
    public string Color { get; private set; }

    public virtual List<SpaceMember> Members { get; private set; } = new();
    
    public virtual List<BusinessFlowVersion> BusinessFlowVersions { get; private set; } = new();
    
    public Space(string name, string description, string color)
    {
        Name = name;
        Description = description;
        Color = color;
    }
    
    public void AddMember(string userId, Enums.SpaceRole role)
    {
        var existedMember = Members.FirstOrDefault(x => x.UserId == userId);
        if (existedMember != null)
        {
            throw new SpaceMemberAlreadyExistedException(userId);
        }
        
        Members.Add(new SpaceMember(userId, role));
    }
    
    public void AddBusinessFlowVersion(BusinessFlowVersion businessFlowVersion)
    {
        BusinessFlowVersions.Add(businessFlowVersion);
    }

    private Space()
    {
        
    }
}