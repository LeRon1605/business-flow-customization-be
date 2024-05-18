using Application.Dtos.Submissions.Responses;
using AutoMapper;
using BuildingBlocks.Application.Cqrs;
using BuildingBlocks.Domain.Repositories;
using Submission.Application.UseCases.Dtos;
using Submission.Domain.FormAggregate.Entities;
using Submission.Domain.FormAggregate.Specifications;

namespace Submission.Application.UseCases.Forms.Queries;

public class GetBusinessFlowBlocksElementsQueryHandler : IQueryHandler<GetBusinessFlowBlocksElementsQuery, BusinessFlowBlocksElementsResponse>
{
    private readonly IBasicReadOnlyRepository<FormElement, int> _formElementRepository;
    private readonly IMapper _mapper;
    
    public GetBusinessFlowBlocksElementsQueryHandler(IBasicReadOnlyRepository<FormElement, int> formElementRepository
        , IMapper mapper)
    {
        _formElementRepository = formElementRepository;
        _mapper = mapper;
    }
    
    public async Task<BusinessFlowBlocksElementsResponse> Handle(GetBusinessFlowBlocksElementsQuery request, CancellationToken cancellationToken)
    {
        var specification = new FormElementBySpaceSpecification(request.SpaceId)
            .And(new FormElementByBusinessFlowIdsSpecification(request.BusinessFlowBlockIds));

        var elements = await _formElementRepository.FilterAsync(specification, new FormElementQueryDto());
        return MapToResponse(request.BusinessFlowBlockIds, elements);
    }
    
    private BusinessFlowBlocksElementsResponse MapToResponse(List<Guid> businessFlowBlockIds, IEnumerable<FormElementQueryDto> elements)
    {
        return new BusinessFlowBlocksElementsResponse()
        {
            Elements = businessFlowBlockIds.ToDictionary(x => x
                , x => elements
                    .Where(e => e.BusinessFlowBlockId == x)
                    .Select(e => _mapper.Map<FormElementDto>(e))
                    .ToList())
        };
    }
}