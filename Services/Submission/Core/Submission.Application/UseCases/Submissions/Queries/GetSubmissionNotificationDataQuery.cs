using Application.Dtos.Notifications.Responses;
using BuildingBlocks.Application.Cqrs;

namespace Submission.Application.UseCases.Submissions.Queries;

public class GetSubmissionNotificationDataQuery : IQuery<List<SubmissionNotificationDataDto>>
{
    public int SpaceId { get; set; }
    
    public List<int> SubmissionIds { get; set; }
    
    public GetSubmissionNotificationDataQuery(int spaceId, List<int> submissionIds)
    {
        SpaceId = spaceId;
        SubmissionIds = submissionIds;
    }
}