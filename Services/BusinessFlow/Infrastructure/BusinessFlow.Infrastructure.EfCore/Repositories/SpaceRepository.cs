using BuildingBlocks.Application.Identity;
using BuildingBlocks.Infrastructure.EfCore;
using BuildingBlocks.Infrastructure.EfCore.Repositories;
using BusinessFlow.Domain.SpaceAggregate.Entities;
using BusinessFlow.Domain.SpaceAggregate.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BusinessFlow.Infrastructure.EfCore.Repositories;

public class SpaceRepository : EfCoreRepository<Space, int>, ISpaceRepository
{
    public SpaceRepository(DbContextFactory dbContextFactory, ICurrentUser currentUser) : base(dbContextFactory, currentUser)
    {
        AddInclude(x => x.BusinessFlowVersions);
    }
    
    public Task<Space?> GetByBlockAsync(Guid blockId)
    {
        return GetQueryable()
            .FirstOrDefaultAsync(x => x.BusinessFlowVersions.Any(v => v.Blocks.Any(b => b.Id == blockId)));
    }
}