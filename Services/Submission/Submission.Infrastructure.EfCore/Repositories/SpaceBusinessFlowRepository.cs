using BuildingBlocks.Application.Identity;
using BuildingBlocks.Infrastructure.EfCore;
using BuildingBlocks.Infrastructure.EfCore.Repositories;
using Microsoft.EntityFrameworkCore;
using Submission.Domain.SpaceBusinessFlowAggregate.Entities;
using Submission.Domain.SpaceBusinessFlowAggregate.Repositories;

namespace Submission.Infrastructure.EfCore.Repositories;

public class SpaceBusinessFlowRepository : EfCoreRepository<SpaceBusinessFlow>, ISpaceBusinessFlowRepository
{
    public SpaceBusinessFlowRepository(DbContextFactory dbContextFactory, ICurrentUser currentUser) : base(dbContextFactory, currentUser)
    {
    }

    public Task<SpaceBusinessFlow?> FindBySpaceId(int spaceId)
    {
        return GetQueryable().FirstOrDefaultAsync(x => x.SpaceId == spaceId);
    }
}