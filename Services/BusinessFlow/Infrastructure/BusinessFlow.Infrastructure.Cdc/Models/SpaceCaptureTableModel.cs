using BuildingBlocks.EventBus;
using BuildingBlocks.EventBus.Enums;
using BuildingBlocks.Infrastructure.Cdc.Attributes;
using BuildingBlocks.Infrastructure.Cdc.Models;
using IntegrationEvents.Cdc;

namespace BusinessFlow.Infrastructure.Cdc.Models;

[CdcTableCaptureName("dbo_Space")]
public class SpaceCaptureTableModel : ICaptureTableModel
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;
    
    public string? Description { get; set; }
    
    public string? Color { get; set; }
    
    public int TenantId { get; set; }
    
    public string? CreatedBy { get; set; }
    
    public EntityTrackingIntegrationEvent GetIntegrationEvent(EntityAction action)
    {
        return new SpaceEntityTrackingIntegrationEvent(Id, Name, Description, Color, CreatedBy, TenantId, action);
    }
}