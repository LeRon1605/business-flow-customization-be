using BuildingBlocks.Domain.Services;
using Submission.Domain.SpaceBusinessFlowAggregate.Entities;

namespace Submission.Domain.SpaceBusinessFlowAggregate.DomainServices;

public interface ISpaceBusinessFlowDomainService : IDomainService
{
    Task<SpaceBusinessFlow> AddOrUpdateVersionAsync(int spaceId, int businessFlowVersionId);
}