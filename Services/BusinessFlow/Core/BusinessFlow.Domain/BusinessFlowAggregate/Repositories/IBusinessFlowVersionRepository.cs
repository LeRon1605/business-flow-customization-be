using BuildingBlocks.Domain.Repositories;
using BusinessFlow.Domain.BusinessFlowAggregate.Entities;

namespace BusinessFlow.Domain.BusinessFlowAggregate.Repositories;

public interface IBusinessFlowVersionRepository : IRepository<BusinessFlowVersion>
{
    Task<TOut?> GetLatestPublishedBusinessFlowAsync<TOut>(int spaceId, IProjection<BusinessFlowVersion, int, TOut> projection);
}