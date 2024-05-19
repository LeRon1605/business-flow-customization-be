using BuildingBlocks.Application.Cqrs;
using Submission.Application.UseCases.Submissions.Dtos;

namespace Submission.Application.UseCases.Submissions.Queries;

public class GetExecutionSubmissionQuery : IQuery<SubmissionDto>
{
    public int ExecutionId { get; set; }
    
    public GetExecutionSubmissionQuery(int executionId)
    {
        ExecutionId = executionId;
    }
}