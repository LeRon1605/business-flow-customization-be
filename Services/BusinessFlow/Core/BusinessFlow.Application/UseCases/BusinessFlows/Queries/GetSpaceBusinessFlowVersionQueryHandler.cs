using BuildingBlocks.Application.Cqrs;
using BuildingBlocks.Domain.Repositories;
using BusinessFlow.Application.UseCases.BusinessFlows.Dtos;
using BusinessFlow.Domain.BusinessFlowAggregate.Entities;
using BusinessFlow.Domain.BusinessFlowAggregate.Specifications;

namespace BusinessFlow.Application.UseCases.BusinessFlows.Queries;

public class GetSpaceBusinessFlowVersionQueryHandler : IQueryHandler<GetSpaceBusinessFlowVersionQuery, IList<BusinessFlowVersionDto>>
{
    private readonly IBasicReadOnlyRepository<BusinessFlowVersion, int> _businessFlowRepository;
    
    public GetSpaceBusinessFlowVersionQueryHandler(IBasicReadOnlyRepository<BusinessFlowVersion, int> businessFlowRepository)
    {
        _businessFlowRepository = businessFlowRepository;
    }
    
    public async Task<IList<BusinessFlowVersionDto>> Handle(GetSpaceBusinessFlowVersionQuery request, CancellationToken cancellationToken)
    {
        var specification = new SpaceBusinessFlowVersionSpecification(request.Id);
        var businessFlowVersions = await _businessFlowRepository.FilterAsync(specification, new BusinessFlowVersionDto());
        
        return businessFlowVersions.OrderByDescending(x => x.Id).ToList();
    }
}