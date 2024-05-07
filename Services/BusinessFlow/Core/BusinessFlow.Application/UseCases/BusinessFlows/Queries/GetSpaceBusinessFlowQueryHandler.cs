using BuildingBlocks.Application.Cqrs;
using BuildingBlocks.Domain.Repositories;
using BusinessFlow.Application.UseCases.BusinessFlows.Dtos;
using BusinessFlow.Domain.BusinessFlowAggregate.Entities;
using BusinessFlow.Domain.BusinessFlowAggregate.Exceptions;
using BusinessFlow.Domain.BusinessFlowAggregate.Specifications;

namespace BusinessFlow.Application.UseCases.BusinessFlows.Queries;

public class GetSpaceBusinessFlowQueryHandler : IQueryHandler<GetSpaceBusinessFlowQuery, BusinessFlowDto>
{
    private readonly IBasicReadOnlyRepository<BusinessFlowVersion, int> _businessFlowRepository;
    
    public GetSpaceBusinessFlowQueryHandler(IBasicReadOnlyRepository<BusinessFlowVersion, int> businessFlowRepository)
    {
        _businessFlowRepository = businessFlowRepository;
    }
    
    public async Task<BusinessFlowDto> Handle(GetSpaceBusinessFlowQuery request, CancellationToken cancellationToken)
    {
        var specification = new SpaceBusinessFlowVersionSpecification(request.SpaceId)
            .And(new BusinessFlowByIdSpecification(request.Id));

        var businessFlowVersion = await _businessFlowRepository.FindAsync(specification, new BusinessFlowDto());
        if (businessFlowVersion == null)
        {
            throw new BusinessFlowNotFoundException(request.Id);
        }

        return businessFlowVersion; 
    }
}