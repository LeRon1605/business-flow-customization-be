using BuildingBlocks.Domain.Repositories;
using BusinessFlow.Domain.BusinessFlowAggregate.Entities;

namespace BusinessFlow.Domain.BusinessFlowAggregate.Repositories;

public interface IBusinessFlowVersionRepository : IRepository<BusinessFlowVersion, int>
{
    Task<TOut?> GetLatestPublishedBusinessFlowAsync<TOut>(int spaceId, IProjection<BusinessFlowVersion, int, TOut> projection);
    
    Task<List<TOut>> GetLatestPublishedBusinessFlowAsync<TOut>(int spaceId, IProjection<BusinessFlowBlock, Guid, TOut> projection);
}