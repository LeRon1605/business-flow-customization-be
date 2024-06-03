using Application.Dtos.Notifications.Responses;
using BuildingBlocks.Application.Cqrs;

namespace Submission.Application.UseCases.Submissions.Queries;

public class GetSubmissionNotificationDataQuery : IQuery<List<SubmissionNotificationDataDto>>
{
    public List<int> SubmissionIds { get; set; }
    
    public GetSubmissionNotificationDataQuery(List<int> submissionIds)
    {
        SubmissionIds = submissionIds;
    }
}