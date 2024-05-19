using BuildingBlocks.Domain.Repositories;
using Submission.Domain.FormAggregate.Entities;

namespace Submission.Domain.FormAggregate.Repositories;

public interface IFormVersionRepository : IRepository<FormVersion>
{
    Task<TOut?> GetLatestSpaceVersionAsync<TOut>(int spaceId, IProjection<FormVersion, TOut> projection);
    
    Task<TOut?> GetAsync<TOut>(int spaceId, int versionId, IProjection<FormVersion, TOut> projection);
    
    Task<TOut?> GetAsync<TOut>(Guid businessFlowBlockId, IProjection<FormVersion, TOut> projection);

    Task<List<TOut>> GetBySpaceAsync<TOut>(int spaceId, IProjection<FormVersion, TOut> projection);
}