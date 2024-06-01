using BuildingBlocks.Application.Identity;
using BuildingBlocks.Domain.Repositories;
using BuildingBlocks.Infrastructure.EfCore;
using BuildingBlocks.Infrastructure.EfCore.Repositories;
using BusinessFlow.Domain.SubmissionExecutionAggregate.Entities;
using BusinessFlow.Domain.SubmissionExecutionAggregate.Enums;
using BusinessFlow.Domain.SubmissionExecutionAggregate.Repositories;
using BusinessFlow.Domain.SubmissionExecutionAggregate.Specifications;
using Microsoft.EntityFrameworkCore;

namespace BusinessFlow.Infrastructure.EfCore.Repositories;

public class SubmissionExecutionRepository : EfCoreRepository<SubmissionExecution>, ISubmissionExecutionRepository
{
    public SubmissionExecutionRepository(DbContextFactory dbContextFactory, ICurrentUser currentUser) : base(dbContextFactory, currentUser)
    {
        AddInclude(x => x.Tasks);
        AddInclude(x => x.PersonInCharges);
    }

    public Task<bool> IsHasFormAsync(int executionId)
    {
        return GetQueryable()
            .AnyAsync(x => x.BusinessFlowBlock.FormId.HasValue && x.Id == executionId);
    }

    public Task<SubmissionExecution?> GetExecutedAsync(int submissionId, Guid outComeId)
    {
        return GetQueryable()
            .FirstOrDefaultAsync(x => x.SubmissionId == submissionId 
                           && x.OutComeId.HasValue 
                           && x.OutComeId == outComeId);
    }

    public async Task<IList<TOut>> GetByPersonInChargeAsync<TOut>(string userId, IProjection<SubmissionExecution, TOut> projection)
    {
        var specification = new AssignedSubmissionExecutionSpecification(userId);

        return await GetQueryable()
            .Where(x => x.Status == SubmissionExecutionStatus.InProgress)
            .Where(specification.ToExpression())
            .Select(projection.GetProject())
            .ToListAsync();
    }
}