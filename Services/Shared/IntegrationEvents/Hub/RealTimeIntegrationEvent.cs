using BuildingBlocks.EventBus;
using Domain.Enums;
using Newtonsoft.Json;

namespace IntegrationEvents.Hub;

public class RealTimeIntegrationEvent : IntegrationEvent
{
    public NotificationType Type { get; set; }
    
    public string Data { get; set; }
    
    public List<string> ReceiverIds { get; set; }
    
    [System.Text.Json.Serialization.JsonConstructor]
    public RealTimeIntegrationEvent(NotificationType type
        , string data
        , List<string> receiverIds)
    {
        Type = type;
        Data = data;
        ReceiverIds = receiverIds;
    }
    
    public RealTimeIntegrationEvent(NotificationType type
        , object data
        , List<string> receiverIds)
    {
        Type = type;
        Data = JsonConvert.SerializeObject(data);
        ReceiverIds = receiverIds;
    }
}