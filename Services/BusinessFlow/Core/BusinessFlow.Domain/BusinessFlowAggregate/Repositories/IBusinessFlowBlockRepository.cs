using BuildingBlocks.Domain.Repositories;
using BusinessFlow.Domain.BusinessFlowAggregate.Entities;

namespace BusinessFlow.Domain.BusinessFlowAggregate.Repositories;

public interface IBusinessFlowBlockRepository : IRepository<BusinessFlowBlock, Guid>
{
    Task<BusinessFlowBlock?> GetStartBlockAsync(int businessFlowVersionId);
    
    Task<BusinessFlowBlock?> GetBlockAsync(Guid blockId);
    
    Task<BusinessFlowBlock?> GetBlockByOutComeAsync(Guid outComeId);
    
    Task<BusinessFlowBlock?> GetNextBlockByOutComeAsync(Guid outComeId);
    
    Task<bool> IsHasFormAsync(Guid blockId);
}