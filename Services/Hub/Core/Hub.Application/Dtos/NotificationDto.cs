using Domain.Enums;
using Hub.Domain.NotificationAggregate.Enums;

namespace Hub.Application.Dtos;

public class NotificationDto
{
    public Guid Id { get; set; }
    
    public string Title { get; set; } = null!;
    
    public string Content { get; set; } = null!;
    
    public NotificationType Type { get; set; } 
    
    public NotificationStatus Status { get; set; }
    
    public string MetaData { get; set; } = null!;
    
    public string SenderId { get; set; } = null!;
    
    public DateTime Created { get; set; }
}