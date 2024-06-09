using BuildingBlocks.Domain.Events;
using BusinessFlow.Domain.SpaceAggregate.Entities;

namespace BusinessFlow.Domain.BusinessFlowAggregate.DomainEvents;

public class SpaceBasicInfoUpdateDomainEvent: IDomainEvent
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Color { get; set; }
    public SpaceBasicInfoUpdateDomainEvent(string name, string description, string color)
    {
        Name = name;
        Description = description;
        Color = color;
    }
}