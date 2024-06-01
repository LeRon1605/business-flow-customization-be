using Application.Dtos.Submissions.Responses;
using BuildingBlocks.Application.Cqrs;

namespace Submission.Application.UseCases.Submissions.Queries;

public class GetSubmissionDataQuery : IQuery<IList<BasicSubmissionDto>>
{
    public List<int> SubmissionIds { get; set; } 
    
    public GetSubmissionDataQuery(List<int> submissionIds)
    {
        SubmissionIds = submissionIds;
    }
}