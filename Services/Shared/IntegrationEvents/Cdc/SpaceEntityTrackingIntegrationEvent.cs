using System.Text.Json.Serialization;
using BuildingBlocks.EventBus;
using BuildingBlocks.EventBus.Enums;

namespace IntegrationEvents.Cdc;

public class SpaceEntityTrackingIntegrationEvent : EntityTrackingIntegrationEvent
{
    public int Id { get; set; }

    public string Name { get; set; }
    
    public string? Description { get; set; }
    
    public string? Color { get; set; }
    
    [JsonConstructor]
    public SpaceEntityTrackingIntegrationEvent()
    {
    }
    
    public SpaceEntityTrackingIntegrationEvent(int id
        , string name
        , string? description
        , string? color
        , string? userId
        , int? tenantId
        , EntityAction action)
    {
        Id = id;
        Name = name;
        Description = description;
        Color = color;
        UserId = userId;
        TenantId = tenantId;
        Action = action;
    }
}