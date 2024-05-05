using BuildingBlocks.Application.Cqrs;
using BuildingBlocks.Domain.Repositories;
using BusinessFlow.Application.UseCases.Spaces.Dtos;
using BusinessFlow.Domain.SpaceAggregate.Entities;

namespace BusinessFlow.Application.UseCases.Spaces.Queries;

public class GetTenantSpacesQueryHandler : IQueryHandler<GetTenantSpacesQuery, List<SpaceDto>>
{
    private readonly IBasicReadOnlyRepository<Space, int> _spaceRepository;
    
    public GetTenantSpacesQueryHandler(IBasicReadOnlyRepository<Space, int> spaceRepository)
    {
        _spaceRepository = spaceRepository;
    }
    
    public async Task<List<SpaceDto>> Handle(GetTenantSpacesQuery request, CancellationToken cancellationToken)
    {
        var spaces = await _spaceRepository.FindAllAsync(new SpaceDto());
        return spaces.ToList();
    }
}