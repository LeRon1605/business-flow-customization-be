namespace Application.Dtos.Notifications.Requests;

public class GetSubmissionNotificationDataRequestDto
{
    public int SpaceId { get; set; }
    
    public List<int> SubmissionIds { get; set; } = null!;
}