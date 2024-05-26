namespace Application.Dtos.Notifications.Models;

public class NotificationPersonInChargeAssignedModel
{
    public int SpaceId { get; set; }
    
    public Guid BusinessFlowBlockId { get; set; }
    
    public int SubmissionId { get; set; }
}