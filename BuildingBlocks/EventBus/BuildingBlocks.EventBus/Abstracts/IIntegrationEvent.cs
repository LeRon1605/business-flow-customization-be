namespace BuildingBlocks.EventBus.Abstracts;

public interface IIntegrationEvent
{
    public string? UserId { get; set; }
    
    public int? TenantId { get; set; }
}