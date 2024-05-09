using BuildingBlocks.EventBus;

namespace IntegrationEvents.BusinessFlow;

public class SpaceBusinessFlowVersionCreatedIntegrationEvent : IntegrationEvent
{
    public int SpaceId { get; set; }
    
    public int BusinessFlowVersionId { get; set; }

    public SpaceBusinessFlowVersionCreatedIntegrationEvent(int spaceId, int businessFlowVersionId)
    {
        SpaceId = spaceId;
        BusinessFlowVersionId = businessFlowVersionId;
    }
}