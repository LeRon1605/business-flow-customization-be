using BuildingBlocks.Domain.Services;

namespace BusinessFlow.Domain.SubmissionExecutionAggregate.DomainServices;

public interface IBusinessFlowExecutorDomainService : IDomainService
{
    Task StartAsync(int businessFlowVersionId, int submissionId);
    
    Task MoveNextAsync(int submissionId, Guid outComeId, string completedBy);
}