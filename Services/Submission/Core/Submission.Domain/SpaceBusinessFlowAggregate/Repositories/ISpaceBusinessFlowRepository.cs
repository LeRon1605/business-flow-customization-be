using BuildingBlocks.Domain.Repositories;
using Submission.Domain.SpaceBusinessFlowAggregate.Entities;

namespace Submission.Domain.SpaceBusinessFlowAggregate.Repositories;

public interface ISpaceBusinessFlowRepository : IRepository<SpaceBusinessFlow>
{
    Task<SpaceBusinessFlow?> FindBySpaceId(int spaceId);
}