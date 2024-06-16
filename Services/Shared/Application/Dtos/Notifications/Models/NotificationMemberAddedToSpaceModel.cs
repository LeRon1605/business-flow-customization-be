namespace Application.Dtos.Notifications.Models;

public class NotificationMemberAddedToSpaceModel
{
    public int SpaceId { get; set; }
    
    public string SpaceName { get; set; } = null!;
    
    public string MemberId { get; set; } = null!;
}