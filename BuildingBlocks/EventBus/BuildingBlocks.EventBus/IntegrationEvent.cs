using BuildingBlocks.EventBus.Abstracts;

namespace BuildingBlocks.EventBus;

public class IntegrationEvent : IIntegrationEvent
{
    public string? UserId { get; set; }
    public int? TenantId { get; set; }
}