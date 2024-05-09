using BuildingBlocks.Domain.Events;
using BusinessFlow.Domain.BusinessFlowAggregate.Entities;

namespace BusinessFlow.Domain.BusinessFlowAggregate.DomainEvents;

public class BusinessFlowVersionCreatedDomainEvent : IDomainEvent
{
    public BusinessFlowVersion BusinessFlowVersion { get; set; }
    
    public BusinessFlowVersionCreatedDomainEvent(BusinessFlowVersion businessFlowVersion)
    {
        BusinessFlowVersion = businessFlowVersion;
    }
}