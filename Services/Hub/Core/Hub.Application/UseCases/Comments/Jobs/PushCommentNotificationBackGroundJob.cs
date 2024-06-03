using BuildingBlocks.Application.Schedulers;

namespace Hub.Application.UseCases.Comments.Jobs;

public class PushCommentNotificationBackGroundJob : BackGroundJob
{
    public Guid CommentId { get; set; }

    public PushCommentNotificationBackGroundJob(Guid commentId)
    {
        CommentId = commentId;
    }
}