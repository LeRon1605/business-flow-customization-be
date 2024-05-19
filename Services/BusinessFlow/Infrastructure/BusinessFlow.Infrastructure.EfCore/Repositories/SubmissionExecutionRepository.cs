using BuildingBlocks.Application.Identity;
using BuildingBlocks.Infrastructure.EfCore;
using BuildingBlocks.Infrastructure.EfCore.Repositories;
using BusinessFlow.Domain.SubmissionExecutionAggregate.Entities;
using BusinessFlow.Domain.SubmissionExecutionAggregate.Repositories;
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
}