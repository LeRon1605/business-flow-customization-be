using Application.Dtos.Spaces;
using BuildingBlocks.Application.Cqrs;
using BuildingBlocks.Application.Identity;
using BuildingBlocks.Domain.Repositories;
using BusinessFlow.Domain.SpaceAggregate.Entities;
using BusinessFlow.Domain.SpaceAggregate.Specifications;

namespace BusinessFlow.Application.UseCases.Spaces.Queries;

public class GetTenantSpacesQueryHandler : IQueryHandler<GetTenantSpacesQuery, List<SpaceDto>>
{
    private readonly IBasicReadOnlyRepository<Space, int> _spaceRepository;
    private readonly ICurrentUser _currentUser;
    
    public GetTenantSpacesQueryHandler(IBasicReadOnlyRepository<Space, int> spaceRepository, ICurrentUser currentUser)
    {
        _spaceRepository = spaceRepository;
        _currentUser = currentUser;
    }
    
    public async Task<List<SpaceDto>> Handle(GetTenantSpacesQuery request, CancellationToken cancellationToken)
    {
        var specification = new SpaceByMemberSpecification(_currentUser.Id);
        
        var spaces = await _spaceRepository
            .FilterAsync(specification, x => new SpaceDto()
            {
                Id = x.Id,
                Name = x.Name,
                Color = x.Color
            });
        
        return spaces.ToList();
    }
}