using BuildingBlocks.Domain.Models;
using Domain.Enums;
using Hub.Domain.NotificationAggregate.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Hub.Domain.NotificationAggregate.Entities;

public class Notification : AuditableTenantAggregateRoot<Guid>
{
    public string Title { get; private set; }
    
    public string Content { get; private set; }
    
    public NotificationType Type { get; private set; }
    
    public NotificationStatus Status { get; private set; }
    
    public string MetaData { get; private set; }
    
    public string SenderId { get; private set; }
    
    public string ReceiverId { get; private set; }
    
    public Notification(string title
        , string content
        , NotificationType type
        , string senderId
        , string receiverId
        , dynamic metaData)
    {
        Id = Guid.NewGuid();
        Title = title;
        Type = type;
        Content = content;
        SenderId = senderId;
        ReceiverId = receiverId;
        MetaData = JsonConvert.SerializeObject(metaData, new JsonSerializerSettings()
        {
            Formatting = Formatting.Indented,
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            }
        });
        Status = NotificationStatus.UnRead;
    }
    
    public void MarkAsRead()
    {
        Status = NotificationStatus.Read;
    }

    private Notification()
    {
        
    }
}