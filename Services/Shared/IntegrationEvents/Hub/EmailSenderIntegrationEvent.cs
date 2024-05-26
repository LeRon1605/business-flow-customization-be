using BuildingBlocks.EventBus;
using BuildingBlocks.Shared.Extensions;

namespace IntegrationEvents.Hub;

public class EmailSenderIntegrationEvent : IntegrationEvent
{
    public string Subject { get; set; }
    public string ToAddress { get; set; }
    public string TemplateName { get; set; }
    public Dictionary<string, string> Data { get; set; }
    
    [System.Text.Json.Serialization.JsonConstructor]
    public EmailSenderIntegrationEvent(string subject, string toAddress, string templateName, Dictionary<string, string> data)
    {
        Subject = subject;
        ToAddress = toAddress;
        TemplateName = templateName;
        Data = data;
    }
    
    public EmailSenderIntegrationEvent(string subject, string toAddress, string templateName, object data)
    {
        Subject = subject;
        ToAddress = toAddress;
        TemplateName = templateName;
        Data = data.ToDictionary();
    }
}