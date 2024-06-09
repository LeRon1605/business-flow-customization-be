using BuildingBlocks.Domain.Events;

namespace BusinessFlow.Domain.BusinessFlowAggregate.DomainEvents;

public class AddMemberInSpaceEvent : IDomainEvent
{
    public string SpaceName { get; set; }
    public string UserId { get; set; }
    public string Role { get; set; }
    
    public AddMemberInSpaceEvent(string spaceName, string userId, string role)
    {
        SpaceName = spaceName;
        UserId = userId;
        Role = role;
    }
}