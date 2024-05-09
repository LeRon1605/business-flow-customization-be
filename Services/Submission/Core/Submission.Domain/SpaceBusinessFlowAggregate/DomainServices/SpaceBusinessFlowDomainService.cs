using Submission.Domain.SpaceBusinessFlowAggregate.Entities;
using Submission.Domain.SpaceBusinessFlowAggregate.Repositories;
using Submission.Domain.SpaceBusinessFlowAggregate.Specifications;

namespace Submission.Domain.SpaceBusinessFlowAggregate.DomainServices;

public class SpaceBusinessFlowDomainService : ISpaceBusinessFlowDomainService
{
    private readonly ISpaceBusinessFlowRepository _spaceBusinessFlowRepository;
    
    public SpaceBusinessFlowDomainService(ISpaceBusinessFlowRepository spaceBusinessFlowRepository)
    {
        _spaceBusinessFlowRepository = spaceBusinessFlowRepository;
    }


    public async Task<SpaceBusinessFlow> AddOrUpdateVersionAsync(int spaceId, int businessFlowVersionId)
    {
        var spaceBusinessFlow = await _spaceBusinessFlowRepository.FindBySpaceId(spaceId);
        if (spaceBusinessFlow == null)
        {
            spaceBusinessFlow = new SpaceBusinessFlow(spaceId, businessFlowVersionId);
            await _spaceBusinessFlowRepository.InsertAsync(spaceBusinessFlow);

            return spaceBusinessFlow;
        }
        
        spaceBusinessFlow.Update(businessFlowVersionId);
        return spaceBusinessFlow;
    }
}