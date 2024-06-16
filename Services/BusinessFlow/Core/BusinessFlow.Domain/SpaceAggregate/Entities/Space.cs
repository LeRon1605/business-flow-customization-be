using BuildingBlocks.Domain.Models;
using BusinessFlow.Domain.BusinessFlowAggregate.Entities;
using BusinessFlow.Domain.SpaceAggregate.DomainEvents;
using BusinessFlow.Domain.SpaceAggregate.Exceptions;

namespace BusinessFlow.Domain.SpaceAggregate.Entities;

public class Space : FullAuditableTenantAggregateRoot
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
        AddDomainEvent(new MemberAddedToSpaceDomainEvent(Id, userId));
    }
    
    public void AddBusinessFlowVersion(BusinessFlowVersion businessFlowVersion)
    {
        BusinessFlowVersions.Add(businessFlowVersion);
    }
    
    public void UpdateSpaceBasicInfo(string name, string description, string color)
    {
        Name = name;
        Description = description;
        Color = color;
    }

    public void UpdateRoleSpaceMember(string userId, int role)
    {
        var existingMember = Members.FirstOrDefault(x => x.UserId == userId);
        if (existingMember == null)
        {
            throw new SpaceMemberNotFoundException(userId);
        }

        Members.Remove(existingMember);

        var newMember = new SpaceMember(userId, (Enums.SpaceRole)role);
        Members.Add(newMember);
    }
    
    public void RemoveMember(string userId)
    {
        var existingMember = Members.FirstOrDefault(x => x.UserId == userId);
        if (existingMember == null)
        {
            throw new SpaceMemberNotFoundException(userId);
        }

        Members.Remove(existingMember);
    }
    
    private Space()
    {
        
    }
}