namespace Application.Dtos.Notifications.Responses;

public class SubmissionNotificationDataDto
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;
    
    public int FormVersionId { get; set; }
    
    public int SpaceId { get; set; }
}