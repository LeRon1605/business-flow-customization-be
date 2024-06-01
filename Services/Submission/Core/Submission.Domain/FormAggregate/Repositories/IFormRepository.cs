using BuildingBlocks.Domain.Repositories;
using Submission.Domain.FormAggregate.Entities;

namespace Submission.Domain.FormAggregate.Repositories;

public interface IFormRepository : IRepository<Form>
{
    Task<Form?> FindByBusinessFlowBlockIdAsync(Guid businessFlowBlockId);
    
    Task<List<TOut>> GetBySpacesAsync<TOut>(List<int> spaceIds, IProjection<FormVersion, TOut> projection);
}