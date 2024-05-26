using BuildingBlocks.Application.Schedulers;

namespace BusinessFlow.Application.UseCases.BusinessFlows.Jobs;

public class SubmissionExecutionPublishIntegrationEventJob : BackGroundJob
{
    public int SubmissionExecutionId { get; set; }
    
    public SubmissionExecutionPublishIntegrationEventJob(int submissionExecutionId)
    {
        SubmissionExecutionId = submissionExecutionId;
    }
}