using BuildingBlocks.Domain.Repositories;
using Submission.Domain.SubmissionAggregate.Entities;

namespace Submission.Domain.SubmissionAggregate.Repositories;

public interface IFormSubmissionExecutionRepository : IRepository<FormSubmissionExecution>
{
    Task<FormSubmissionExecution?> FindByFormSubmissionIdAsync(int formSubmissionId);
}