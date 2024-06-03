namespace Application.Dtos.Notifications.Requests;

public class GetSubmissionNotificationDataRequestDto
{
    public List<int> SubmissionIds { get; set; } = null!;
}