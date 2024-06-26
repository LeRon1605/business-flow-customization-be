using BuildingBlocks.Application.Identity;
using BuildingBlocks.Domain.Repositories;
using BuildingBlocks.Infrastructure.EfCore;
using BuildingBlocks.Infrastructure.EfCore.Repositories;
using BusinessFlow.Domain.BusinessFlowAggregate.Entities;
using BusinessFlow.Domain.BusinessFlowAggregate.Enums;
using BusinessFlow.Domain.BusinessFlowAggregate.Repositories;
using BusinessFlow.Domain.BusinessFlowAggregate.Specifications;
using LinqKit;
using Microsoft.EntityFrameworkCore;

namespace BusinessFlow.Infrastructure.EfCore.Repositories;

public class BusinessFlowVersionRepository : EfCoreRepository<BusinessFlowVersion, int>, IBusinessFlowVersionRepository
{
    public BusinessFlowVersionRepository(DbContextFactory dbContextFactory, ICurrentUser currentUser) : base(dbContextFactory, currentUser)
    {
    }

    public async Task<TOut?> GetLatestPublishedBusinessFlowAsync<TOut>(int spaceId, IProjection<BusinessFlowVersion, int, TOut> projection)
    {
        var specification = new SpaceBusinessFlowVersionSpecification(spaceId)
            .And(new PublishedBusinessFlowVersionSpecification());

        return await GetQueryable(specification)
            .OrderByDescending(x => x.Id)
            .Select(projection.GetProject().Expand())
            .FirstOrDefaultAsync();
    }

    public Task<List<TOut>> GetLatestPublishedBusinessFlowAsync<TOut>(int spaceId, IProjection<BusinessFlowBlock, Guid, TOut> projection)
    {
        var specification = new SpaceBusinessFlowVersionSpecification(spaceId)
            .And(new PublishedBusinessFlowVersionSpecification());

        return GetQueryable()
            .Where(specification.ToExpression())
            .OrderByDescending(x => x.Id)
            .Take(1)
            .SelectMany(x => x.Blocks)
            .Where(x => x.Type != BusinessFlowBlockType.Start)
            .Select(projection.GetProject().Expand())
            .ToListAsync();
    }
}