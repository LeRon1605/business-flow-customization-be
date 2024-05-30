using BuildingBlocks.Domain.Repositories;
using BusinessFlow.Domain.SubmissionExecutionAggregate.Entities;

namespace BusinessFlow.Domain.SubmissionExecutionAggregate.Repositories;

public interface ISubmissionExecutionRepository : IRepository<SubmissionExecution>
{
    Task<bool> IsHasFormAsync(int executionId);
    
    Task<SubmissionExecution?> GetExecutedAsync(int submissionId, Guid outComeId);
}