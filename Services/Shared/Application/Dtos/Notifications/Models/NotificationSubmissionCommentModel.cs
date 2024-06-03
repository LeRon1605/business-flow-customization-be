namespace Application.Dtos.Notifications.Models;

public class NotificationSubmissionCommentModel
{
    public Guid Id { get; set; }
    
    public int SubmissionId { get; set; }
    
    public string Content { get; set; } = null!;
}