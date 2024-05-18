using BuildingBlocks.Application.Cqrs;
using BuildingBlocks.Domain.Repositories;
using BusinessFlow.Application.Clients.Abstracts;
using BusinessFlow.Application.UseCases.BusinessFlows.Dtos;
using BusinessFlow.Domain.BusinessFlowAggregate.Entities;
using BusinessFlow.Domain.BusinessFlowAggregate.Exceptions;
using BusinessFlow.Domain.BusinessFlowAggregate.Specifications;

namespace BusinessFlow.Application.UseCases.BusinessFlows.Queries;

public class GetSpaceBusinessFlowQueryHandler : IQueryHandler<GetSpaceBusinessFlowQuery, BusinessFlowDto>
{
    private readonly IBasicReadOnlyRepository<BusinessFlowVersion, int> _businessFlowRepository;
    private readonly ISubmissionClient _submissionClient;
    
    public GetSpaceBusinessFlowQueryHandler(IBasicReadOnlyRepository<BusinessFlowVersion, int> businessFlowRepository
        , ISubmissionClient submissionClient)
    {
        _businessFlowRepository = businessFlowRepository;
        _submissionClient = submissionClient;
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
        
        await MapBusinessFlowBlocksElements(request.SpaceId, businessFlowVersion);

        return businessFlowVersion; 
    }
    
    private async Task MapBusinessFlowBlocksElements(int spaceId, BusinessFlowDto businessFlow)
    {
        var businessFlowBlockIds = businessFlow.Blocks.Select(b => b.Id).ToList();
        var response = await _submissionClient.GetBusinessFlowBlocksElementsAsync(spaceId, businessFlowBlockIds);
        
        foreach (var block in businessFlow.Blocks)
        {
            block.Elements = response.Elements[block.Id];
        }
    }
}