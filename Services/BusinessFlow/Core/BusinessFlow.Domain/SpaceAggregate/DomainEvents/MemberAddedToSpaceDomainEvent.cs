using BuildingBlocks.Domain.Events;

namespace BusinessFlow.Domain.SpaceAggregate.DomainEvents;

public class MemberAddedToSpaceDomainEvent : IDomainEvent
{
    public int SpaceId { get; set; }
    public string UserId { get; set; }
    
    public MemberAddedToSpaceDomainEvent(int spaceId, string userId)
    {
        SpaceId = spaceId;
        UserId = userId;
    }
}