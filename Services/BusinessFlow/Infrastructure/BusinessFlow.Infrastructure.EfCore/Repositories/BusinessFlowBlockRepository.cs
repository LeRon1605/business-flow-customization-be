using BuildingBlocks.Application.Identity;
using BuildingBlocks.Infrastructure.EfCore;
using BuildingBlocks.Infrastructure.EfCore.Repositories;
using BusinessFlow.Domain.BusinessFlowAggregate.Entities;
using BusinessFlow.Domain.BusinessFlowAggregate.Enums;
using BusinessFlow.Domain.BusinessFlowAggregate.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BusinessFlow.Infrastructure.EfCore.Repositories;

public class BusinessFlowBlockRepository : EfCoreRepository<BusinessFlowBlock, Guid>, IBusinessFlowBlockRepository
{
    public BusinessFlowBlockRepository(DbContextFactory dbContextFactory, ICurrentUser currentUser) : base(dbContextFactory, currentUser)
    {
    }

    public Task<BusinessFlowBlock?> GetStartBlockAsync(int businessFlowVersionId)
    {
        return GetQueryable()
            .Include(x => x.TaskSettings)
            .Include(x => x.PersonInChargeSettings)
            .Where(x => x.BusinessFlowVersionId == businessFlowVersionId 
                        && x.ToBranches.Any(y => y.FromBlock.Type == BusinessFlowBlockType.Start))
            .FirstOrDefaultAsync();
    }

    public Task<BusinessFlowBlock?> GetBlockAsync(Guid blockId)
    {
        return GetQueryable()
            .Include(x => x.OutComes)
            .FirstOrDefaultAsync(x => x.Id == blockId);
    }

    public Task<BusinessFlowBlock?> GetBlockByOutComeAsync(Guid outComeId)
    {
        return GetQueryable()
            .Where(x => x.OutComes.Any(o => o.Id == outComeId))
            .FirstOrDefaultAsync();
    }

    public Task<BusinessFlowBlock?> GetNextBlockByOutComeAsync(Guid outComeId)
    {
        return GetQueryable()
            .Include(x => x.TaskSettings)
            .Include(x => x.PersonInChargeSettings)
            .Where(x => x.ToBranches.Any(y => y.OutComeId == outComeId))
            .FirstOrDefaultAsync();
    }

    public Task<bool> IsHasFormAsync(Guid blockId)
    {
        return GetQueryable()
            .AnyAsync(x => x.Id == blockId && x.FormId.HasValue);
    }
}