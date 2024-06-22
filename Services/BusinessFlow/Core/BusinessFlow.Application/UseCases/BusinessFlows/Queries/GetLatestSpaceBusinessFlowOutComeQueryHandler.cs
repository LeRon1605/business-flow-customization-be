using BuildingBlocks.Application.Cqrs;
using BusinessFlow.Application.UseCases.BusinessFlows.Dtos;
using BusinessFlow.Domain.BusinessFlowAggregate.Repositories;

namespace BusinessFlow.Application.UseCases.BusinessFlows.Queries;

public class GetLatestSpaceBusinessFlowOutComeQueryHandler : IQueryHandler<GetLatestSpaceBusinessFlowOutComeQuery, List<BusinessFlowBlockOutComeDto>>
{
    private readonly IBusinessFlowVersionRepository _businessFlowVersionRepository;
    
    public GetLatestSpaceBusinessFlowOutComeQueryHandler(IBusinessFlowVersionRepository businessFlowVersionRepository)
    {
        _businessFlowVersionRepository = businessFlowVersionRepository;
    }
    
    public Task<List<BusinessFlowBlockOutComeDto>> Handle(GetLatestSpaceBusinessFlowOutComeQuery request, CancellationToken cancellationToken)
    {
        return _businessFlowVersionRepository.GetLatestPublishedBusinessFlowAsync<BusinessFlowBlockOutComeDto>(request.SpaceId, new BusinessFlowBlockOutComeDto());
    }
}