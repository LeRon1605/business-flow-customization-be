using BuildingBlocks.EventBus.Enums;

namespace BuildingBlocks.EventBus;

public class EntityTrackingIntegrationEvent : IntegrationEvent
{
    public EntityAction Action { get; set; }
}