using BuildingBlocks.Application.Identity;
using BuildingBlocks.Infrastructure.EfCore;
using BuildingBlocks.Infrastructure.EfCore.Repositories;
using BusinessFlow.Domain.SpaceAggregate.Entities;
using BusinessFlow.Domain.SpaceAggregate.Repositories;

namespace BusinessFlow.Infrastructure.EfCore.Repositories;

public class SpaceRepository : EfCoreRepository<Space>, ISpaceRepository
{
    public SpaceRepository(DbContextFactory dbContextFactory, ICurrentUser currentUser) : base(dbContextFactory, currentUser)
    {
        AddInclude(x => x.BusinessFlowVersions);
    }
}