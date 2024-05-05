using BuildingBlocks.Application.Identity;
using BuildingBlocks.Infrastructure.EfCore;
using BuildingBlocks.Infrastructure.EfCore.Repositories;
using BusinessFlow.Domain.BusinessFlowAggregate.Entities;
using BusinessFlow.Domain.BusinessFlowAggregate.Repositories;

namespace BusinessFlow.Infrastructure.EfCore.Repositories;

public class BusinessFlowVersionRepository : EfCoreRepository<BusinessFlowVersion>, IBusinessFlowVersionRepository
{
    public BusinessFlowVersionRepository(DbContextFactory dbContextFactory, ICurrentUser currentUser) : base(dbContextFactory, currentUser)
    {
    }
}