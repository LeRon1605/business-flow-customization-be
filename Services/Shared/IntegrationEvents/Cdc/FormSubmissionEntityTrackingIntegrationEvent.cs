using BuildingBlocks.EventBus;
using BuildingBlocks.EventBus.Enums;
using Newtonsoft.Json;

namespace IntegrationEvents.Cdc;

public class FormSubmissionEntityTrackingIntegrationEvent : EntityTrackingIntegrationEvent
{
    public int Id { get; set; }
    
    public string Name { get; set; } = null!;
    
    [JsonConstructor]
    public FormSubmissionEntityTrackingIntegrationEvent()
    {
    }
    
    public FormSubmissionEntityTrackingIntegrationEvent(int id
        , string name
        , string? userId
        , int? tenantId
        , EntityAction action)
    {
        Id = id;
        Name = name;
        UserId = userId;
        TenantId = tenantId;
        Action = action;
    }
}