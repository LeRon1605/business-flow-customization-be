using BuildingBlocks.EventBus.Abstracts;

namespace IntegrationEvents.Hub;

public class EmailSenderIntegrationEvent : IIntegrationEvent
{
    public string Subject { get; set; }
    public string ToAddress { get; set; }
    public string TemplateName { get; set; }
    public dynamic Data { get; set; }
    
    public EmailSenderIntegrationEvent(string subject, string toAddress, string templateName, dynamic data)
    {
        Subject = subject;
        ToAddress = toAddress;
        TemplateName = templateName;
        Data = data;
    }
}