using BuildingBlocks.Application.Cqrs;
using BusinessFlow.Application.UseCases.BusinessFlows.Dtos;

namespace BusinessFlow.Application.UseCases.BusinessFlows.Queries;

public class GetSubmissionExecutionBusinessFlowQuery : IQuery<IList<SubmissionExecutionBusinessFlowDto>>
{
    public int SubmissionId { get; set; }
    
    public GetSubmissionExecutionBusinessFlowQuery(int submissionId)
    {
        SubmissionId = submissionId;
    }
}