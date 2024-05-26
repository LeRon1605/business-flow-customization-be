namespace Application.Dtos.Notifications.Requests;

public class GetBusinessFlowNotificationDataRequestDto
{
    public int SpaceId { get; set; }

    public List<GetBusinessFlowNotificationDataEntityRequestDto> Entities { get; set; } = null!;
}

public class GetBusinessFlowNotificationDataEntityRequestDto
{
    public string Id { get; set; } = null!;
    
    public BusinessFlowNotificationDataType Type { get; set; }
}

public enum BusinessFlowNotificationDataType
{
    Space,
    BusinessFlow,
    Execution
}