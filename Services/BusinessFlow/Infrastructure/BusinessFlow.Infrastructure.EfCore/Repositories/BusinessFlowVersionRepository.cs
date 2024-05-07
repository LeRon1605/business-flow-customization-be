using BuildingBlocks.Application.Identity;
using BuildingBlocks.Domain.Repositories;
using BuildingBlocks.Infrastructure.EfCore;
using BuildingBlocks.Infrastructure.EfCore.Repositories;
using BusinessFlow.Application.UseCases.BusinessFlows.Dtos;
using BusinessFlow.Domain.BusinessFlowAggregate.Entities;
using BusinessFlow.Domain.BusinessFlowAggregate.Repositories;
using BusinessFlow.Domain.BusinessFlowAggregate.Specifications;
using Microsoft.EntityFrameworkCore;

namespace BusinessFlow.Infrastructure.EfCore.Repositories;

public class BusinessFlowVersionRepository : EfCoreRepository<BusinessFlowVersion>, IBusinessFlowVersionRepository
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
            .Select(projection.GetProject())
            .FirstOrDefaultAsync();
    }
}