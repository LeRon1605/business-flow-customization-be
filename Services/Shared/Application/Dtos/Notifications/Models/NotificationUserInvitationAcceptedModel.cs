namespace Application.Dtos.Notifications.Models;

public class NotificationUserInvitationAcceptedModel
{
    public string UserId { get; set; } = null!;

    public string FullName { get; set; } = null!;
    
    public int TenantId { get; set; }
    
    public string TenantName { get; set; } = null!;
}