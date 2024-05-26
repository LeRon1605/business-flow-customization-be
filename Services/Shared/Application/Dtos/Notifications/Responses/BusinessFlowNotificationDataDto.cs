using Application.Dtos.Notifications.Requests;

namespace Application.Dtos.Notifications.Responses;

public class BusinessFlowNotificationDataDto
{
    public object Id { get; set; } = null!;
    
    public string Name { get; set; } = null!;
    
    public BusinessFlowNotificationDataType Type { get; set; }
}