using BuildingBlocks.EventBus;
using BuildingBlocks.EventBus.Enums;

namespace BuildingBlocks.Infrastructure.Cdc.Models;

public interface ICaptureTableModel
{
    int Id { get; set; }
    
    EntityTrackingIntegrationEvent GetIntegrationEvent(EntityAction action);
}