using BuildingBlocks.Application.Cqrs;
using Submission.Application.UseCases.Submissions.Dtos;

namespace Submission.Application.UseCases.Submissions.Queries;

public class GetSubmissionByTrackingTokenQuery : IQuery<SubmissionDto>
{
    public string TrackingToken { get; set; }
    
    public GetSubmissionByTrackingTokenQuery(string trackingToken)
    {
        TrackingToken = trackingToken;
    }
}