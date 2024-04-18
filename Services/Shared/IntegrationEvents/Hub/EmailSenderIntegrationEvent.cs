using BuildingBlocks.EventBus.Abstracts;

namespace IntegrationEvents.Hub;

public class EmailSenderIntegrationEvent : IIntegrationEvent
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
    
    public EmailSenderIntegrationEvent(string subject, string toAddress, string templateName, dynamic data)
    {
        Subject = subject;
        ToAddress = toAddress;
        TemplateName = templateName;
        Data = new Dictionary<string, string>();
        
        var properties = data.GetType().GetProperties();
        foreach (var property in properties)
        {
            var name = property.Name;
            var value = data.GetType().GetProperty(property.Name).GetValue(data, null);
            
            Data.Add(name, value?.ToString() ?? string.Empty);
        }
    }
}